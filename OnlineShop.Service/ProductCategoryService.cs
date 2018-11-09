using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Service
{
    public interface IProductCategoryService
    {
        ProductCategory Add(ProductCategory productCategory);
        void Update(ProductCategory productCategory);
        ProductCategory Delete(int id);
        IEnumerable<ProductCategory> GetAll();
        IEnumerable<ProductCategory> GetAll(string keyword);
        void Save();
    }

    public class ProductCategoryService : IProductCategoryService
    {
        IProductCategoryRepository _productCategory;
        IUnitOfWork _unitOfWork;

        public ProductCategoryService(IProductCategoryRepository productCategory, IUnitOfWork unitOfWork)
        {
            this._productCategory = productCategory;
            this._unitOfWork = unitOfWork;
        }

        public ProductCategory Add(ProductCategory productCategory)
        {
            return _productCategory.Add(productCategory);
        }

        public ProductCategory Delete(int id)
        {
            return _productCategory.Delete(id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _productCategory.GetAll();
        }

        public IEnumerable<ProductCategory> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _productCategory.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _productCategory.GetAll();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductCategory productCategory)
        {
            _productCategory.Update(productCategory);
        }
    }
}
