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
    public interface IProductService
    {
        Product Add(Product product);
        void Update(Product product);
        Product GetSingleProduct(int id);
        Product Delete(int id);
        IEnumerable<Product> Getall();
        IEnumerable<Product> Getall(string keyword);
        void Save();
    }

    public class ProductService : IProductService
    {
        IProductRepository _productRepository;
        IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this._productRepository = productRepository;
            this._unitOfWork = unitOfWork;
        }

        public Product Add(Product product)
        {
            return _productRepository.Add(product);
        }

        public Product Delete(int id)
        {
            return _productRepository.Delete(id);
        }

        public IEnumerable<Product> Getall()
        {
            return _productRepository.GetAll();
        }

        public IEnumerable<Product> Getall(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _productRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _productRepository.GetAll();
        }

        public Product GetSingleProduct(int id)
        {
            return _productRepository.GetSingleEntity(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Product product)
        {
            _productRepository.Update(product);
        }
    }
}
