﻿using NeatBurger.Models.Entities;

namespace NeatBurger.Models.ViewModels
{
    public class MenuViewModel
    {
        public Menu Menu { get; set; } = null!;
        public IEnumerable<ClasificacionModel> ListaClasificaciones { get; set; } = null!;
    }

    public class ClasificacionModel
    {
        public string Clasificacion { get; set; } = null!;
        public IEnumerable<HamburguesaModel> ListaHamburguesas { get; set; } = null!;
    }

    public class HamburguesaModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; } 
    }
}
