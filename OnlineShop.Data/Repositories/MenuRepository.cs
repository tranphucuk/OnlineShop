using OnlineShop.Data.Infrastructure;
using OnlineShop.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Data.Repositories
{
    public interface IMenuRepository
    {

    }

    public class MenuRepository : RepositoryBase<Menu>,IMenuRepository
    {
        public MenuRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
