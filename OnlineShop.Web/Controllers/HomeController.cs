﻿using AutoMapper;
using OnlineShop.Model.Model;
using OnlineShop.Service;
using OnlineShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace OnlineShop.Web.Controllers
{
    public class HomeController : Controller
    {
        IProductCategoryService _productCategory;
        ICommonService _commonService;
        IProductService _productService;

        public HomeController(IProductCategoryService productCategory, ICommonService commonService,
            IProductService productService)
        {
            this._productCategory = productCategory;
            this._commonService = commonService;
            this._productService = productService;
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
            var footer = _commonService.GetFooter();
            var footerViewModel = Mapper.Map<Footer, FooterViewModel>(footer);
            return PartialView(footerViewModel);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Category()
        {
            var listProductCategory = _productCategory.GetAll();
            var listProductCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(listProductCategory);
            return PartialView(listProductCategoryViewModel);
        }
    }
}