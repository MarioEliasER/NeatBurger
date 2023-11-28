using Microsoft.AspNetCore.Mvc;
using NeatBurger.Models.ViewModels;
using NeatBurger.Repositories;
using NuGet.Protocol.Core.Types;

namespace NeatBurger.Controllers
{
    public class HomeController : Controller
    {
        MenuRepository repo { get; }
        public HomeController(MenuRepository repository)
        {
            repo = repository;
        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Promociones()
        {

            return View();
        }

        public IActionResult Menu(string Id)
        {
            if (Id == null)
            {
                var lista = repo.GetClasificaciones();
                MenuViewModel vm = new()
                {
                    ListaClasificacion = lista.ToList()
                };
            }
            else
            {

            }
            return View();
        }
    }
}
