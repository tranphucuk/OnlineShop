using OnlineShop.Data.Infrastructure;
using OnlineShop.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Data.Repositories
{
    public interface ISystemConfigRepository
    {

    }

    public class SystemConfigRepository : RepositoryBase<SystemConfig>
    {
        public SystemConfigRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
