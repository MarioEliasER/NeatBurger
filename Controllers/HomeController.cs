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

        public IActionResult Promociones(int Id)
        {
            int indice = 0;
            var lista = Repo.GetAll().Where(x=>x.PrecioPromocion != null && x.Id != Id);
            
            
            return View();
        }

        public IActionResult Menu(string Id)
        {
            MenuViewModel vm = new();
            if (Id != null)
            {
                Id = Id.Replace("-", " ");
                vm.Menu = Repo.GetXNombre(Id);
                vm.ListaClasificaciones = Repo.GetData().GroupBy(x => x.IdClasificacionNavigation).Select(x => new ClasificacionModel()
                {
                    Clasificacion = x.Key.Nombre,
                    ListaMenu = x.Where(y => y.IdClasificacion == x.Key.Id).Select(x => new MenusModel()
                    {
                        Id = x.Id,
                        Nombre = x.Nombre,
                        Precio = (decimal)x.Precio,
                    }).ToList()
                });
            }
            else
            {
                vm.Menu = Repo.GetData().OrderBy(x => x.Nombre).First();
                vm.ListaClasificaciones = Repo.GetData().GroupBy(x => x.IdClasificacionNavigation).Select(x => new ClasificacionModel()
                {
                    Clasificacion = x.Key.Nombre,
                    ListaMenu = x.Where(y => y.IdClasificacion == x.Key.Id).Select(x => new MenusModel()
                    {
                        Id = x.Id,
                        Nombre = x.Nombre,
                        Precio = (decimal)x.Precio,
                    }).ToList()
                });
            }
            return View(vm);
        }
    }
}