using OnlineShop.Data.Infrastructure;
using OnlineShop.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Data.Repositories
{
    public interface IEmailRepository : IRepository<Email>
    {

    }

    public class EmailRepository : RepositoryBase<Email>, IEmailRepository
    {
        public EmailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
