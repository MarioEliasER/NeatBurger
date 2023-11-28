using Microsoft.EntityFrameworkCore;
using NeatBurger.Models.Entities;

namespace NeatBurger.Repositories
{
    public class MenuRepository : Repository<Menu>
    {
        public MenuRepository(NeatContext context) : base(context)
        {
            
        }

        public IEnumerable<Menu> GetMenuByClasificacion()
        {
            return Context.Menu
                .Include(x => x.IdClasificacionNavigation);
        }

        public IEnumerable<Clasificacion> GetClasificaciones()
        {
            return Context.Clasificacion.OrderBy(x => x.Id);
        }
    }
}
