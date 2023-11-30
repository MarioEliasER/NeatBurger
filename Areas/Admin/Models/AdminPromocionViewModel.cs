namespace NeatBurger.Areas.Admin.Models
{
    public class AdminPromocionViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public double Precio { get; set; }
        public double? PrecioPromocion { get; set; }
    }
}
