using Microsoft.EntityFrameworkCore;
using NeatBurger.Models.Entities;

namespace NeatBurger.Repositories
{
    public class MenuRepository : Repository<Menu>
    {
        public MenuRepository(NeatContext context) : base(context)
        {
            
        }

        public IEnumerable<Menu> GetMenuByClasificacion(string clasificacion)
        {
            return Context.Menu
                .Include(x => x.IdClasificacionNavigation)
                .Where(x => x.IdClasificacionNavigation != null && x.IdClasificacionNavigation.Nombre == clasificacion)
                .OrderBy(x => x.Nombre);
        }
    }
}
