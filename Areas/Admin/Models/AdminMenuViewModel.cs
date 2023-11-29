using NeatBurger.Models.Entities;
using NeatBurger.Models.ViewModels;

namespace NeatBurger.Areas.Admin.Models
{
    public class AdminMenuViewModel
    {
        public IEnumerable<ClasificacionAdminModel> ListaClasificaciones { get; set; } = null!;
    }

    public class ClasificacionAdminModel
    {
        public string Clasificacion { get; set; } = null!;
        public IEnumerable<Menu> ListaHamburguesas { get; set; } = null!;
    }
}
