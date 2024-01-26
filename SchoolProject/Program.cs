using Microsoft.EntityFrameworkCore;
using SchoolProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


string connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/accessdenied";
    });
builder.Services.AddAuthorization();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapGet("/accessdenied", async (HttpContext context) =>
{
    context.Response.StatusCode = 403;
    await context.Response.WriteAsync("Access Denied");
});
app.MapGet("/login", async (HttpContext context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    // html-форма для ввода логина/пароля
    string loginForm = @"<!DOCTYPE html>
    <html>
    <head>
        <meta charset='utf-8' />
        <title>METANIT.COM</title>
    </head>
    <body>
        <h2>Login Form</h2>
        <form method='post'>
            <p>
                <label>Login</label><br />
                <input name='login' />
            </p>
            <p>
                <label>Password</label><br />
                <input type='password' name='password' />
            </p>
            <input type='submit' value='Login' />
        </form>
    </body>
    </html>";
    await context.Response.WriteAsync(loginForm);
});

app.MapPost("/login", async (string? returnUrl, HttpContext context, ApplicationContext db) =>
{
    // получаем из формы login и пароль
    var form = context.Request.Form;
    // если login и/или пароль не установлены, посылаем статусный код ошибки 400
    if (!form.ContainsKey("login") || !form.ContainsKey("password"))
        return Results.BadRequest("Email и/или пароль не установлены");
    string login = form["login"];
    string password = form["password"];

    // находим пользователя 
    User? user = db.Users.FirstOrDefault(p => p.Login == login && p.Password == password);
    // если пользователь не найден, отправляем статусный код 401
    if (user is null) return Results.Unauthorized();
    var claims = new List<Claim>
    {
        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
        new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
    };
    var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
    await context.SignInAsync(claimsPrincipal);
    return Results.Redirect(returnUrl ?? "/userinfo");
});

app.Map("/userinfo", (HttpContext context) =>
{
    var login = context.User.FindFirst(ClaimsIdentity.DefaultNameClaimType);
    var role = context.User.FindFirst(ClaimsIdentity.DefaultRoleClaimType);
    return $"Name: {login?.Value}\nRole: {role?.Value}";
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "student_id",
    pattern: "{controller=User}/{action=Index}/{id}");

app.Run();
