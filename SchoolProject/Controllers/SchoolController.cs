using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Models;
using System.Diagnostics;

namespace SchoolProject.Controllers
{
    public class SchoolController : Controller
    {
        private readonly ILogger<SchoolController> _logger;
        ApplicationContext db;
        public SchoolController(ApplicationContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Create()
        {
            db.Schools.Add(new School(1, "МБОУ СОШ 3", "Троицк"));
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View(db.Schools.ToList());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}