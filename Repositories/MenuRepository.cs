using NeatBurger.Models.Entities;

namespace NeatBurger.Repositories
{
    public class MenuRepository : Repository<Menu>
    {
        public MenuRepository(NeatContext context) : base(context)
        {
        }
    }
}
