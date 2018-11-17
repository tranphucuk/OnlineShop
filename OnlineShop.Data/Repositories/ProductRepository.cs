using OnlineShop.Data.Infrastructure;
using OnlineShop.Model.Model;

namespace OnlineShop.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {

    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}