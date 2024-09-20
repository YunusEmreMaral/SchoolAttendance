using Microsoft.AspNetCore.Mvc;
using SchoolAttendance_UI.Models;
using System.Diagnostics;

namespace SchoolAttendance_UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Kullanıcının kimliğini kontrol et
            if (User.Identity.IsAuthenticated)
            {
                // Kullanıcı giriş yapmış
                return View();
            }
            else
            {
                // Kullanıcı giriş yapmamış
                return RedirectToAction("Login", "Account");
            }
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
