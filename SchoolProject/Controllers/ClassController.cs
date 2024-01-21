using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Models;
using System.Text;

namespace SchoolProject.Controllers
{
    public class ClassController : Controller
    {

        ApplicationContext db;
        public ClassController(ApplicationContext context)
        {
            db = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await db.Classes.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            db.Classes.Add(new Class(11, 'A', "Физмат"));
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}
