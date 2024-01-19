using Microsoft.AspNetCore.Mvc;
using SchoolProject.Models;
using System.Diagnostics;

namespace SchoolProject.Controllers
{
    public class SchoolController : Controller
    {
        private readonly ILogger<SchoolController> _logger;

        public SchoolController(ILogger<SchoolController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return Content("Hello in my School platform");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}