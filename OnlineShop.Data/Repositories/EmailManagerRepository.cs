using OnlineShop.Data.Infrastructure;
using OnlineShop.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Data.Repositories
{
    public interface IEmailManagerRepository : IRepository<EmailManager>
    {

    }

    public class EmailManagerRepository : RepositoryBase<EmailManager>, IEmailManagerRepository
    {
        public EmailManagerRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
