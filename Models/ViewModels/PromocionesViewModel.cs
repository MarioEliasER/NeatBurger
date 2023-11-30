using NeatBurger.Models.Entities;

namespace NeatBurger.Models.ViewModels
{
    public class PromocionesViewModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Descripción { get; set; } = null!;

        public double Precio { get; set; }

        public double? PrecioPromocion { get; set; }

        public string MenuAnterior { get; set; } = null!;

        public string MenuSiguiente { get; set; } = null!;
    }
}
