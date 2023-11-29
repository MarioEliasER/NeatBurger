using Microsoft.AspNetCore.Mvc;
using NeatBurger.Areas.Admin.Models;
using NeatBurger.Models.Entities;
using NeatBurger.Repositories;

namespace NeatBurger.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        MenuRepository Repo { get; }
        public HomeController(MenuRepository repository)
        {
            Repo = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Menu()
        {
            AdminMenuViewModel vm = new()
            {
                ListaClasificaciones = Repo.GetData().GroupBy(x => x.IdClasificacionNavigation)
                .Select(x => new ClasificacionModel
                {
                    Clasificacion = x.Key.Nombre,
                    ListaHamburguesas = Repo.GetData().Where(y => y.IdClasificacion == x.Key.Id)
                })
            };
            return View(vm);
        }
    }
}
