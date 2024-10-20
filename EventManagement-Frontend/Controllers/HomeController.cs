using EventManagement_Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EventManagement_Frontend.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ILogger<HomeController> _logger;

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
        public IActionResult EventAppPage()
        {
            return View();
        }

        public IActionResult HomePage(LoginModel model)
        {
            TempData.Keep();
            return View(model);
        }
    }
}
