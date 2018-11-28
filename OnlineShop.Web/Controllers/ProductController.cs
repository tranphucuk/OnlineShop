using AutoMapper;
using OnlineShop.Common;
using OnlineShop.Model.Model;
using OnlineShop.Service;
using OnlineShop.Web.Infrastructure.Core;
using OnlineShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Web.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;
        IProductCategoryService _productCategoryService;

        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            this._productService = productService;
            this._productCategoryService = productCategoryService;
        }
        // GET: Product
        public ActionResult Detail(int id)
        {
            return View();
        }

        public ActionResult Category(int id, string sort, int page = 1)
        {
            var totalRow = 0;
            var productCategory = _productCategoryService.GetById(id);
            var productCategoryViewModel = Mapper.Map<ProductCategory, ProductCategoryViewModel>(productCategory);
            ViewBag.Category = productCategoryViewModel;
            ViewBag.sortType = sort;

            var pageSize = int.Parse(ConfigHelper.GetValueByKey("pageSize"));
            var productList = _productService.GetProductByCategoryIdPaging(id, sort, page, pageSize, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productList);
            var pageginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                Page = page,
                TotalCount = totalRow,
                TotalPage = (int)Math.Ceiling((double)totalRow / pageSize),
                MaxPage = int.Parse(ConfigHelper.GetValueByKey("maxPage"))
            };
            return View(pageginationSet);
        }

        public ActionResult Search(string keyword, string sort, int page = 1)
        {
            var totalRow = 0;
            ViewBag.sortType = sort;
            ViewBag.productName = keyword;

            var pageSize = int.Parse(ConfigHelper.GetValueByKey("pageSize"));
            var productList = _productService.GetListProductByKeyword(keyword, sort, page, pageSize, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productList);
            var pageginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                Page = page,
                TotalCount = totalRow,
                TotalPage = (int)Math.Ceiling((double)totalRow / pageSize),
                MaxPage = int.Parse(ConfigHelper.GetValueByKey("maxPage"))
            };
            return View(pageginationSet);
        }

        public JsonResult GetListProductByName(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword) && !keyword.StartsWith(" "))
            {
                var listProductName = _productService.GetListProductName(keyword);
                return Json(new
                {
                    data = listProductName,
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                data = "",
            }, JsonRequestBehavior.AllowGet);
        }
    }
}