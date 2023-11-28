using NeatBurger.Models.Entities;

namespace NeatBurger.Models.ViewModels
{
    public class MenuViewModel
    {
        public List<Clasificacion> ListaClasificacion { get; set; } = null!;
        public IEnumerable<HamburguesaModel> Hamburguesas { get; set; } = null!;
    }

    public class HamburguesaModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripción { get; set; } = null!;
        public decimal Precio { get; set; } 
    }
}
