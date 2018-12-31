using OnlineShop.Common;
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
        IEnumerable<Product> GetLatestProducts(int size);
        IEnumerable<Product> GetHotProducts();
        IEnumerable<Product> GetProductByCategoryIdPaging(int categoryId, string sort, int page, int pageSize, out int totalRow);
        IEnumerable<Product> GetListProductByKeyword(string keyword, string sort, int page, int pageSize, out int totalRow);
        IEnumerable<Product> GetRelatedProducts(int id, int number);
        IEnumerable<string> GetListProductName(string keyword);
        IEnumerable<Product> GetHotProductsPaging(string sort, int page, int pageSize, out int totalRow);
        IEnumerable<Product> GetLatestProductsPaging(string sort, int page, int pageSize, out int totalRow);

        IEnumerable<Tag> GetListTagsByProductId(int id);
        void IncreaseView(int id);
        IEnumerable<Product> GetListProductByTagId(string tagid, int page, int pageSize, out int totalRow);
        void Save();
    }

    public class ProductService : IProductService
    {
        IProductRepository _productRepository;
        IUnitOfWork _unitOfWork;
        ITagRepository _tagRepository;
        IProductTagRepository _productTagRepository;

        public ProductService(IProductRepository productRepository, IProductTagRepository productTagRepository,
            ITagRepository tagRepository, IUnitOfWork unitOfWork)
        {
            this._productRepository = productRepository;
            this._unitOfWork = unitOfWork;
            this._productTagRepository = productTagRepository;
            this._tagRepository = tagRepository;
        }

        public Product Add(Product product)
        {
            var tempProduct = _productRepository.Add(product);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                for (int i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(t => t.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.productTag;
                        _tagRepository.Add(tag);
                    }
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = product.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                }
            }
            return tempProduct;
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
                return _productRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetHotProducts()
        {
            return _productRepository.GetMulti(x => x.Status == true && x.HotFlag == true).OrderByDescending(x => x.CreatedDate);
        }

        public IEnumerable<Product> GetLatestProducts(int size)
        {
            return _productRepository.GetMulti(x => x.Status == true).OrderByDescending(x => x.CreatedDate).Take(size);
        }

        public IEnumerable<string> GetListProductName(string keyword)
        {
            return _productRepository.GetMulti(x => x.Status == true && x.Name.Contains(keyword)).Select(y => y.Name);
        }

        public IEnumerable<Product> GetProductByCategoryIdPaging(int categoryId, string sort, int page, int pageSize, out int totalRow)
        {
            var query = _productRepository.GetMulti(x => x.Status == true && x.CategoryID == categoryId);
            query = _productRepository.SortProducts(query, sort);
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> GetListProductByKeyword(string keyword, string sort, int page, int pageSize, out int totalRow)
        {
            var query = _productRepository.GetMulti(x => x.Status == true && x.Name.Contains(keyword));
            query = _productRepository.SortProducts(query, sort);
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
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
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                for (int i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(t => t.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.productTag;
                        _tagRepository.Add(tag);
                    }
                    _productTagRepository.DeleteMulti(x => x.ProductID == product.ID);
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = product.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                }
            }
        }

        public IEnumerable<Product> GetRelatedProducts(int id, int number)
        {
            var product = _productRepository.GetSingleEntity(id);
            return _productRepository.GetMulti(x => x.Status == true && x.ID != id && x.CategoryID == product.CategoryID)
                .OrderByDescending(x => x.CreatedDate).Take(number);
        }

        public IEnumerable<Tag> GetListTagsByProductId(int id)
        {
            return _productTagRepository.GetMulti(x => x.ProductID == id, new string[] { "Tag" }).Select(y => y.Tag);
        }

        public void IncreaseView(int id)
        {
            var product = _productRepository.GetSingleEntity(id);
            if (product.ViewCount.HasValue)
                product.ViewCount += 1;
            else
                product.ViewCount = 1;
        }

        public IEnumerable<Product> GetListProductByTagId(string tagid, int page, int pageSize, out int totalRow)
        {
            var model = _productRepository.GetProductsByTagId(tagid);
            totalRow = model.Count();
            return model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> GetHotProductsPaging(string sort, int page, int pageSize, out int totalRow)
        {
            var query = _productRepository.GetMulti(x => x.Status == true && x.HotFlag == true);
            query = _productRepository.SortProducts(query, sort).OrderByDescending(x => x.CreatedDate);
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> GetLatestProductsPaging(string sort, int page, int pageSize, out int totalRow)
        {
            var query = _productRepository.GetMulti(x => x.Status == true);
            query = _productRepository.SortProducts(query, sort).OrderByDescending(x => x.CreatedDate);
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
