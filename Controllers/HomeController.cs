using Microsoft.AspNetCore.Mvc;
using NeatBurger.Models.Entities;
using NeatBurger.Models.ViewModels;
using NeatBurger.Repositories;
using NuGet.Protocol.Core.Types;

namespace NeatBurger.Controllers
{
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

        public IActionResult Promociones()
        {

            return View();
        }

        public IActionResult Menu(string Id)
        {
            if (Id != null)
            {
                Id = Id.Replace("-", " ");
                MenuViewModel vm = new()
                {
                    Menu = Repo.GetXNombre(Id),
                    ListaClasificaciones = Repo.GetData().GroupBy(x => x.IdClasificacionNavigation).Select(x => new ClasificacionModel()
                    {
                        Clasificacion = x.Key.Nombre,
                        ListaHamburguesas = x.Where(y => y.IdClasificacion == x.Key.Id).Select(x => new HamburguesaModel()
                        {
                            Id = x.Id,
                            Nombre = x.Nombre,
                            Precio = (decimal)x.Precio,
                        }).ToList()
                    })
                };
                return View(vm);
            }
            else
            {
                MenuViewModel vm = new()
                {
                    Menu = Repo.GetData().OrderBy(x => x.Nombre).First(),
                    ListaClasificaciones = Repo.GetData().GroupBy(x => x.IdClasificacionNavigation).Select(x => new ClasificacionModel()
                    {
                        Clasificacion = x.Key.Nombre,
                        ListaHamburguesas = x.Where(y => y.IdClasificacion == x.Key.Id).Select(x => new HamburguesaModel()
                        {
                            Id = x.Id,
                            Nombre = x.Nombre,
                            Precio = (decimal)x.Precio,
                        }).ToList()
                    })
                };
                return View(vm);
            }
        }
    }
}
