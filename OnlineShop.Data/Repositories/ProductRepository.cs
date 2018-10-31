using OnlineShop.Data.Infrastructure;
using OnlineShop.Model.Model;

namespace OnlineShop.Data.Repositories
{
    public class ProductRepository : RepositoryBase<Product>
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}