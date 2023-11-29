using Microsoft.AspNetCore.Mvc;
using NeatBurger.Areas.Admin.Models;
using NeatBurger.Models.Entities;
using NeatBurger.Repositories;

namespace NeatBurger.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController : Controller
    {
        MenuRepository Repo { get; }
        Repository<Clasificacion> ClasifRepo { get; }
        public MenuController(MenuRepository repository, Repository<Clasificacion> ClasifRepo)
        {
            Repo = repository;
            this.ClasifRepo = ClasifRepo;
        }

        public IActionResult Index()
        {
            AdminMenuViewModel vm = new()
            {
                ListaClasificaciones = Repo.GetData().GroupBy(x => x.IdClasificacionNavigation)
                .Select(x => new ClasificacionAdminModel
                {
                    Clasificacion = x.Key.Nombre,
                    ListaHamburguesas = Repo.GetData().Where(y => y.IdClasificacion == x.Key.Id)
                })
            };
            return View(vm);
        }

        public IActionResult Agregar()
        {
            AdminAgregarMenuViewModel vm = new()
            {
                Clasificaciones = ClasifRepo.GetAll().OrderBy(x => x.Id).Select(x => new ClasificacionModel 
                { 
                    Id = x.Id,
                    Nombre = x.Nombre
                })
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Agregar(AdminAgregarMenuViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.Menu.Nombre))
            {
                ModelState.AddModelError("", "El nombre no puede estar vacío.");
            }
            if (vm.Menu.Precio == 0)
            {
                ModelState.AddModelError("", "El nombre no puede estar vacío.");
            }
            if (string.IsNullOrWhiteSpace(vm.Menu.Descripción))
            {
                ModelState.AddModelError("", "La descripción no puede estar vacía.");
            }
            if (vm.Archivo != null)
            {
                if (vm.Archivo.ContentType != "image/png")
                {
                    ModelState.AddModelError("", "Solo se permite subir imagenes en formato PNG.");
                }
                if (vm.Archivo.Length > 500 * 1024)
                {
                    ModelState.AddModelError("", "Solo se permiten archivos no mayores a 500Kb");
                }
            }
            if (ModelState.IsValid)
            {
                Repo.Insert(vm.Menu);
                if (vm.Archivo != null)
                {
                    FileStream fs = System.IO.File.Create($"wwwroot/hamburguesas/{vm.Menu.Id}.png");
                    vm.Archivo.CopyTo(fs);
                    fs.Close();
                }
                else
                {
                    System.IO.File.Copy("wwwroot/images/burger.png", $"wwwroot/hamburguesas/{vm.Menu.Id}.png");
                }
            }
            vm.Clasificaciones = ClasifRepo.GetAll().OrderBy(x => x.Id).Select(x => new ClasificacionModel
            {
                Id = x.Id,
                Nombre = x.Nombre
            });
            return View(vm);
        }
    }
}
