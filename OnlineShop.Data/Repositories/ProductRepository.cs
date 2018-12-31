using OnlineShop.Data.Infrastructure;
using OnlineShop.Model.Model;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetProductsByTagId(string tagId);
        IEnumerable<Product> SortProducts(IEnumerable<Product> products, string sortType);
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

        public IEnumerable<Product> SortProducts(IEnumerable<Product> products, string sortType)
        {
            switch (sortType)
            {
                case "popular":
                    products = products.OrderByDescending(x => x.ViewCount);
                    break;
                case "price":
                    products = products.OrderBy(x => x.Price);
                    break;
                case "discount":
                    products = products.OrderBy(x => x.PromotionPrice.HasValue);
                    break;
                case "hot":
                    products = products.OrderBy(x => x.HotFlag == true);
                    break;
                default:
                    break;
            }
            return products;
        }
    }
}