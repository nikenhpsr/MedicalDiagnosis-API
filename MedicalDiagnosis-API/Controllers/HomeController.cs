using MedicalDiagnosis_API.Data;
using MedicalDiagnosis_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MedicalDiagnosis_API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MedicalDiagnosisContext _context;

        public HomeController(ILogger<HomeController> logger, MedicalDiagnosisContext context)
        {
            _logger = logger;
            _context = context;
        }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
