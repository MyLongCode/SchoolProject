using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Models;
using System.Text;

namespace SchoolProject.Controllers
{
    public class SchoolController : Controller
    {
        ApplicationContext db;
        public SchoolController(ApplicationContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Create(School school)
        {
            db.Schools.Add(school);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View(db.Schools.ToList());
        }

    }
}