using Microsoft.AspNetCore.Mvc;
using SchoolProject.Models;
namespace SchoolProject.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            ViewData["countStudents"] = db.Students.Count();
            ViewData["countSchools"] = db.Schools.Count();
            ViewData["countClasses"] = db.Classes.Count();

            return View();
        }
    }
}
