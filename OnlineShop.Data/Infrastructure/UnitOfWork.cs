using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private OnlineShopDbContext dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public OnlineShopDbContext DbContext
        {
            get
            {
                return dbContext ?? (dbContext = dbFactory.Init());
            }
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}
