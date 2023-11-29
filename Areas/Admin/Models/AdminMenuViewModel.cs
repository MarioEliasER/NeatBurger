using NeatBurger.Models.Entities;
using NeatBurger.Models.ViewModels;

namespace NeatBurger.Areas.Admin.Models
{
    public class AdminMenuViewModel
    {
        public IEnumerable<ClasificacionModel> ListaClasificaciones { get; set; } = null!;
    }

    public class ClasificacionModel
    {
        public string Clasificacion { get; set; } = null!;
        public IEnumerable<Menu> ListaHamburguesas { get; set; } = null!;
    }
}
