using Microsoft.AspNetCore.Components.RenderTree;
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

        public IActionResult Promociones(string Id)
        {
            var lista = Repo.GetPromociones().Select(x=> new
            {
                Nombre = x.Nombre
            }).ToArray();
            if (lista != null)
            {
                Id = Id?.Replace("-", " ") ?? lista[0].Nombre;
                var datos = Repo.GetXNombre(Id);
                int indice = Array.FindIndex(lista, x => x.Nombre == Id);
                PromocionesViewModel vm = new();
                vm.Nombre = Id;
                vm.Id = datos?.Id ?? 0;
                vm.Precio = (double)(datos?.Precio ?? 0);
                vm.PrecioPromocion = (double)(datos?.PrecioPromocion?? 0);
                vm.Descripción = datos?.Descripción ?? "N/A";
                vm.MenuAnterior = lista[(indice - 1 + lista.Length) % lista.Length].Nombre;
                vm.MenuSiguiente = lista[(indice + 1) % lista.Length].Nombre;
                return View(vm);
            }
            else
            {
                return RedirectToAction("Index");
            }
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
                    ListaMenu = x.Where(y => y.IdClasificacion == x.Key.Id).OrderBy(x => x.Nombre).Select(x => new MenusModel()
                    {
                        Id = x.Id,
                        Nombre = x.Nombre,
                        Precio = (decimal)x.Precio,
                    }).ToList()
                });
            }
            else
            {
                vm.Menu = Repo.GetData().OrderBy(x => x.Nombre).FirstOrDefault();
                vm.ListaClasificaciones = Repo.GetData().GroupBy(x => x.IdClasificacionNavigation).Select(x => new ClasificacionModel()
                {
                    Clasificacion = x.Key.Nombre,
                    ListaMenu = x.Where(y => y.IdClasificacion == x.Key.Id).OrderBy(x=>x.Nombre).Select(x => new MenusModel()
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