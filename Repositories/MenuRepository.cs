﻿using Microsoft.EntityFrameworkCore;
using NeatBurger.Models.Entities;

namespace NeatBurger.Repositories
{
    public class MenuRepository : Repository<Menu>
    {
        public MenuRepository(NeatContext context) : base(context)
        {
            
        }

        public Menu? GetXNombre(string nombre)
        {
            return Context.Menu.Include(x=>x.IdClasificacionNavigation).FirstOrDefault(x=> x.Nombre == nombre);
        }
        public IEnumerable<Menu> GetData()
        {
            return Context.Menu.Include(x=>x.IdClasificacionNavigation).OrderBy(x => x.Id);
        }

        public IEnumerable<Menu> GetPromociones()
        {
            return Context.Menu.Where(x => x.PrecioPromocion != null || x.PrecioPromocion > 0).OrderBy(x => x.Nombre);
        }
    }
}
