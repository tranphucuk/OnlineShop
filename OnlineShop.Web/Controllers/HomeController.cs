using AutoMapper;
using OnlineShop.Model.Model;
using OnlineShop.Service;
using OnlineShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Web.Controllers
{
    public class HomeController : Controller
    {
        IProductCategoryService _productCategory;
        ICommonService _commonService;

        public HomeController(IProductCategoryService productCategory, ICommonService commonService)
        {
            this._productCategory = productCategory;
            this._commonService = commonService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            var footer = _commonService.GetFooter();
            var footerViewModel = Mapper.Map<Footer, FooterViewModel>(footer);
            return PartialView(footerViewModel);
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Category()
        {
            var listProductCategory = _productCategory.GetAll();
            var listProductCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(listProductCategory);
            return PartialView(listProductCategoryViewModel);
        }
    }
}