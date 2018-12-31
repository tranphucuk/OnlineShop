using OnlineShop.Data.Infrastructure;
using OnlineShop.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Data.Repositories
{
    public interface ILogoRepository : IRepository<Logo>
    {

    }

    public class LogoRepository : RepositoryBase<Logo>, ILogoRepository
    {
        public LogoRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
