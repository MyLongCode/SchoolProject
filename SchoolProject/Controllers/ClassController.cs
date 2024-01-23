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
                Class? _class = await db.Classes.FirstOrDefaultAsync(p => p.Id == id);
                List<Student> students = await db.Students.Where(p => p.ClassId == id).ToListAsync();
                foreach(Student student in students)
                    student.Marks = await db.Marks.Where(p => p.StudentId == student.Id).ToListAsync();
                _class.Students = students;
                return View("ClassInfo", _class);
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
