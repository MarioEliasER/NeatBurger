using NeatBurger.Models.Entities;

namespace NeatBurger.Areas.Admin.Models
{
    public class AdminAgregarMenuViewModel
    {
        public IEnumerable<ClasifModel>? Clasificaciones { get; set; } = null!;
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripción { get; set; } = null!;
        public double Precio { get; set; }
        public double? PrecioPromocion { get; set; }
        public int IdClasificacion { get; set; }
        public IFormFile? Archivo { get; set; }
    }

    public class ClasifModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
