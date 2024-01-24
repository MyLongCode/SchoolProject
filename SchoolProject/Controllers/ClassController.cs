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
        [Route ("Class/{id?}")]
        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            if (id != null)
            {
                return BadRequest ();
            }
            return View(await db.Classes.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Create(Class _class)
        {
            db.Classes.Add(_class);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        
    }
}
