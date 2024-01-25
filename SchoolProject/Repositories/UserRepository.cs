using Microsoft.IdentityModel.Tokens;
using SchoolProject.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchoolProject.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationContext db;
        private string secretKey;

        public UserRepository(ApplicationContext context, IConfiguration configuration)
        {
            db = context;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public bool isUniqueUser(string username)
        {
            var user = db.Users.FirstOrDefault(p => p.Login == username);
            if (user == null) return true;
            return false;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var user = db.Users.FirstOrDefault(u => u.Login.ToLower() == loginRequest.UserName.ToLower() &&
                                                u.Password == loginRequest.Password);
            if (user == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var loginResponse = new LoginResponse()
            {
                Token = tokenHandler.WriteToken(token),
                User = user,
            };
            return loginResponse;
        }

        public async Task<User> Register(RegistrationRequest registrationRequest)
        {
            var user = new User()
            {
                Login = registrationRequest.Login,
                Name = registrationRequest.Name,
                Password = registrationRequest.Password,
                Role = registrationRequest.Role, 
                Classes = registrationRequest.Classes,
            };
            db.Users.Add(user);
            await db.SaveChangesAsync();
            user.Password = "";
            return user;
        }
    }
}
