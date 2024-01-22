using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Models;
using System.Text;

namespace SchoolProject.Controllers
{
    public class StudentController : Controller
    {

        ApplicationContext db;
        public StudentController(ApplicationContext context)
        {
            db = context;
        }

        [Route ("Student/{id?}")]
        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            foreach (var student in db.Students)
            {
                if (student.Class == null)
                {
                    Class? _class = await db.Classes.FirstOrDefaultAsync(p => p.Id == student.ClassId);
                    student.Class = _class;
                    db.SaveChanges();
                }
            }
            if (id is not null)
            {
                Student? student = await db.Students.FirstOrDefaultAsync(p => p.Id == id);
                List<Mark> studentMarks = await db.Marks.Where(p => p.StudentId == student.Id).ToListAsync();
                student.Marks = studentMarks;
                return View("StudentInfo", student);
            }
            return View(await db.Students.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student? student)
        {
            if (student != null)
            {
                db.Students.Add(student);
                await db.SaveChangesAsync();
            }
            else return NotFound();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Student? Student = await db.Students.FirstOrDefaultAsync(p => p.Id == id);
                if (Student != null)
                {
                    db.Students.Remove(Student);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            db.Students.Update(student);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
