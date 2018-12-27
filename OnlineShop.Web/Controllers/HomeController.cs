using AutoMapper;
using OnlineShop.Common.Exceptions;
using OnlineShop.Model.Model;
using OnlineShop.Service;
using OnlineShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Page = OnlineShop.Model.Model.Page;

namespace OnlineShop.Web.Controllers
{
    public class HomeController : Controller
    {
        IProductCategoryService _productCategoryService;
        ICommonService _commonService;
        IProductService _productService;
        IPageService _pageService;
        IEmailService _emailService;

        public HomeController(IProductCategoryService productCategory,
            ICommonService commonService, IPageService pageService, IEmailService emailService,
            IProductService productService)
        {
            this._productCategoryService = productCategory;
            this._commonService = commonService;
            this._productService = productService;
            this._pageService = pageService;
            this._emailService = emailService;
        }

        [OutputCache(CacheProfile = "cache1min")]
        public ActionResult Index()
        {
            var slideModel = _commonService.GetSlides();
            var SlideViewModel = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slideModel);

            var latestProductModels = _productService.GetLatestProducts(3);
            var latestProductViewModels = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(latestProductModels);
            var hotProductModels = _productService.GetHotProducts();
            var hotProductViewModels = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(hotProductModels);

            var homeViewModel = new HomeViewModel();
            homeViewModel.Slides = SlideViewModel;
            homeViewModel.LatestProducts = latestProductViewModels;
            homeViewModel.HotProducts = hotProductViewModels;

            return View(homeViewModel);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600, VaryByParam = "none")]
        public ActionResult Footer()
        {
            var pagesViewModel = Mapper.Map<IEnumerable<Page>, IEnumerable<PageViewModel>>(_pageService.GetAll());
            var productCategoriesViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(_productCategoryService.GetAll());
            var productsViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(_productService.Getall());
            var footerViewModel = new FooterViewModel()
            {
                Pages = pagesViewModel,
                LatestProducts = productsViewModel,
                ProductCategories = productCategoriesViewModel
            };
            return PartialView(footerViewModel);
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Category()
        {
            var listProductCategory = _productCategoryService.GetAll();
            var listProductCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(listProductCategory);
            return PartialView(listProductCategoryViewModel);
        }

        [HttpPost]
        public JsonResult NewsLester(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                return Json(new
                {
                    status = false,
                    data = "Email address is empty."
                });
            }
            var email = new Email()
            {
                CreatedDate = DateTime.Now,
                EmailAddress = emailAddress,
                Status = true
            };
            try
            {
                _emailService.Add(email);
                _emailService.Save();

                return Json(new
                {
                    data = email,
                    status = true
                });
            }
            catch (NameDuplicatedException dex)
            {
                return Json(new
                {
                    status = false,
                    data = dex.Message
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false,
                    data = "Email address is invalid, please check."
                });
            }
        }
    }
}