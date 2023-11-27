using Microsoft.AspNetCore.Mvc;

namespace NeatBurger.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Promociones()
        {

            return View();
        }

        public IActionResult Menu()
        {
            return View();
        }
    }
}
