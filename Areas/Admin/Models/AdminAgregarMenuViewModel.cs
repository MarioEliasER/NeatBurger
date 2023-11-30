using NeatBurger.Models.Entities;

namespace NeatBurger.Areas.Admin.Models
{
    public class AdminAgregarMenuViewModel
    {
        public IEnumerable<ClasifModel>? Clasificaciones { get; set; } = null!;
        public Menu Menu { get; set; } = new();
        public IFormFile? Archivo { get; set; }
    }

    public class ClasifModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
