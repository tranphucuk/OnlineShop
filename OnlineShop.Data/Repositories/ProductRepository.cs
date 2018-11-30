using OnlineShop.Data.Infrastructure;
using OnlineShop.Model.Model;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetProductsByTagId(string tagId);
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Product> GetProductsByTagId(string tagId)
        {
            var query = from p in DbContext.Products
                        join pt in DbContext.ProductTags
                        on p.ID equals pt.ProductID
                        where pt.TagID == tagId
                        select p;

            return query.AsEnumerable();
        }
    }
}