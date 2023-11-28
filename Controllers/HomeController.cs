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
            Id = Id.Replace("-", " ");
            var clasificacion = repo.GetAll().OrderBy(x => x.Id).GroupBy(x => x.IdClasificacionNavigation.Nombre).Select(
                x => new MenuViewModel
                {
                    Clasificacion = x.Key,
                    Hamburguesas = repo.GetMenuByClasificacion(Id).Select(x => new HamburguesaModel
                    {
                        Id = x.Id,
                        Nombre = x.Nombre,
                        Descripción = x.Descripción,
                        Precio = (decimal)x.Precio
                    })
                });
            return View(clasificacion);
        }
    }
}
