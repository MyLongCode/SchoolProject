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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await db.Students.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> CreateX(Student? student)
        {
            if (student != null)
            {
                db.Students.Add(student);
                await db.SaveChangesAsync();
            }
            else return NotFound();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            db.Students.Add(new Student("Данил", "Михайлов", new DateTime(2005, 1, 14), 3424234, 1));
            db.SaveChanges();
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

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
