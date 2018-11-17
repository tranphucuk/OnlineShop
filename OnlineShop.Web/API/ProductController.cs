using AutoMapper;
using OnlineShop.Data.Infrastructure;
using OnlineShop.Model.Model;
using OnlineShop.Service;
using OnlineShop.Web.Infrastructure.Core;
using OnlineShop.Web.Infrastructure.Extensions;
using OnlineShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace OnlineShop.Web.API
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiControllerBase
    {
        IProductService _productService;

        public ProductController(IErrorService errorService, IProductService productService) : base(errorService)
        {
            this._productService = productService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpReponse(request, () =>
             {
                 var model = _productService.Getall(keyword);
                 var totalRow = model.Count();
                 var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                 var responseData = Mapper.Map<List<ProductViewModel>>(query);
                 var pagination = new PaginationSet<ProductViewModel>
                 {
                     Items = responseData.ToList(),
                     Page = page,
                     TotalCount = totalRow,
                     TotalPage = (int)Math.Ceiling((decimal)totalRow / pageSize),
                 };

                 return request.CreateResponse(HttpStatusCode.OK, pagination);
             });
        }

        [Route("getProductId/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetProductCategory(HttpRequestMessage request, int id)
        {
            return CreateHttpReponse(request, () =>
            {
                var productToEdit = _productService.GetSingleProduct(id);
                var responseData = Mapper.Map<ProductViewModel>(productToEdit);
                return request.CreateResponse(HttpStatusCode.OK, responseData);
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductViewModel productVm)
        {
            return CreateHttpReponse(request, () =>
            {
                if (ModelState.IsValid)
                {
                    Product product = new Product();
                    product.UpdateProduct(productVm);

                    _productService.Add(product);
                    _productService.Save();

                    var responseData = Mapper.Map<Product, ProductViewModel>(product);
                    return request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductViewModel productVm)
        {
            return CreateHttpReponse(request, () =>
            {
                if (ModelState.IsValid)
                {
                    Product product = new Product();
                    product.UpdateProduct(productVm);

                    _productService.Update(product);
                    _productService.Save();

                    var responseData = Mapper.Map<Product, ProductViewModel>(product);
                    return request.CreateResponse(HttpStatusCode.OK, responseData);
                }
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            });
        }

        [Route("delete/{id:int}")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpReponse(request, () =>
            {
                if (ModelState.IsValid)
                {
                    var dbModel = _productService.Delete(id);
                    _productService.Save();
                    var responseData = Mapper.Map<Product, ProductViewModel>(dbModel);
                    return request.CreateResponse(HttpStatusCode.OK, responseData);
                }
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            });
        }

        [Route("deleteMulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string idJsonArray)
        {
            return CreateHttpReponse(request, () =>
            {
                if (ModelState.IsValid)
                {
                    var listId = new JavaScriptSerializer().Deserialize<List<int>>(idJsonArray);
                    foreach (var id in listId)
                    {
                        _productService.Delete(id);
                    }
                    _productService.Save();
                    return request.CreateResponse(HttpStatusCode.OK, listId.Count);
                }
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            });
        }
    }
}
