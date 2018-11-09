using AutoMapper;
using OnlineShop.Service;
using OnlineShop.Web.Infrastructure.Core;
using OnlineShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShop.Web.API
{
    [RoutePrefix("api/product_category")]
    public class ProductCategoryController : ApiControllerBase
    {
        IProductCategoryService _productCategoryService;

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService)
            : base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }

        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpReponse(request, () =>
            {
                int totalRow = 0;
                var model = _productCategoryService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<List<ProducatCategoryViewModel>>(query);

                var paginationSet = new PaginationSet<ProducatCategoryViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPage = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                return request.CreateResponse(HttpStatusCode.OK, paginationSet);
            });
        }
    }
}
