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
                Clasificaciones = ClasifRepo.GetAll().OrderBy(x => x.Id).Select(x => new ClasifModel 
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
            if (vm.Menu.Precio <= 0)
            {
                ModelState.AddModelError("", "El precio no puede ser 0 ni ser menor a 0.");
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
                if (vm.Archivo.Length > 5000 * 1024)
                {
                    ModelState.AddModelError("", "Solo se permiten archivos no mayores a 5MB");
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
                return RedirectToAction("Index");
            }
            vm.Clasificaciones = ClasifRepo.GetAll().OrderBy(x => x.Id).Select(x => new ClasifModel
            {
                Id = x.Id,
                Nombre = x.Nombre
            });
            return View(vm);
        }

        public IActionResult Editar(int Id)
        {
            var menu = Repo.Get(Id);
            if (menu == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                AdminAgregarMenuViewModel vm = new();
                vm.Menu = menu;
                vm.Clasificaciones = ClasifRepo.GetAll()
                    .Select(x => new ClasifModel
                    {
                        Id = x.Id,
                        Nombre = x.Nombre
                    });
                return View(vm);
            }
        }

        [HttpPost]
        public IActionResult Editar(AdminAgregarMenuViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.Menu.Nombre))
            {
                ModelState.AddModelError("", "El nombre no puede estar vacío.");
            }
            if (vm.Menu.Precio <= 0)
            {
                ModelState.AddModelError("", "El precio no puede ser 0 ni ser menor a 0.");
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
                if (vm.Archivo.Length > 5000 * 1024)
                {
                    ModelState.AddModelError("", "Solo se permiten archivos no mayores a 5MB");
                }
            }
            if (ModelState.IsValid)
            {
                var menu = Repo.Get(vm.Menu.Id);
                if (menu == null)
                {
                    return RedirectToAction("Index");
                }
                menu.Nombre = vm.Menu.Nombre;
                menu.Descripción = vm.Menu.Descripción;
                menu.Precio = vm.Menu.Precio;
                menu.PrecioPromocion = vm.Menu.PrecioPromocion;
                menu.IdClasificacion = vm.Menu.IdClasificacion;

                Repo.Update(menu);
                if (vm.Archivo != null)
                {
                    System.IO.FileStream fs = System.IO.File.Create($"wwwroot/hamburguesas/{vm.Menu.Id}.png");
                    vm.Archivo.CopyTo(fs);
                    fs.Close();
                }
                return RedirectToAction("Index");
            }
            vm.Clasificaciones = Repo.GetAll()
                .Select(x => new ClasifModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre
                });
            return View(vm);
        }

        public IActionResult Eliminar(int Id)
        {
            var menu = Repo.Get(Id);
            if (menu == null)
            {
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        [HttpPost]
        public IActionResult Eliminar(Menu p)
        {
            var menu = Repo.Get(p.Id);
            if (menu == null)
            {
                return RedirectToAction("Index");
            }
            Repo.Delete(menu);
            var imagen = $"wwwroot/hamburguesas/{p.Id}.png";
            if (System.IO.File.Exists(imagen))
            {
                System.IO.File.Delete(imagen);
            }
            return RedirectToAction("Index");
        }

        public IActionResult AgregarPromocion(int Id)
        {
            var menu = Repo.Get(Id);
            if (menu == null)
            {
                return RedirectToAction("Index");
            }
            AdminPromocionViewModel vm = new()
            {
                Id = menu.Id,
                Nombre = menu.Nombre,
                Precio = menu.Precio,
                PrecioPromocion = menu.PrecioPromocion
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult AgregarPromocion(AdminPromocionViewModel vm)
        {
            if (vm.PrecioPromocion >= vm.Precio)
            {
                ModelState.AddModelError("", "La promoción debe ser menor al precio actual.");
            }
            if (vm.PrecioPromocion <= 0)
            {
                ModelState.AddModelError("", "La promoción no puede ser 0.");
            }
            if (ModelState.IsValid)
            {
                var menu = Repo.Get(vm.Id);
                if (menu == null)
                {
                    return RedirectToAction("Index");
                }
                menu.Nombre = vm.Nombre;
                menu.Precio = vm.Precio;
                menu.PrecioPromocion = vm.PrecioPromocion;
                Repo.Update(menu);
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        public IActionResult QuitarPromocion(int Id)
        {
            var menu = Repo.Get(Id);
            if (menu == null)
            {
                return RedirectToAction("Index");
            }
            AdminPromocionViewModel vm = new()
            {
                Id = menu.Id,
                Nombre = menu.Nombre,
                Precio = menu.Precio,
                PrecioPromocion = menu.PrecioPromocion
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult QuitarPromocion(AdminPromocionViewModel vm)
        {
            var menu = Repo.Get(vm.Id);
            if (menu == null)
            {
                return RedirectToAction("Index");
            }
            menu.Nombre = vm.Nombre;
            menu.Precio = vm.Precio;
            menu.PrecioPromocion = null;
            Repo.Update(menu);
            return RedirectToAction("Index");
        }
    }
}
