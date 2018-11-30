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
    public class PageController : Controller
    {
        // GET: Page
        IPageService _pageService;
        public PageController(IPageService pageService)
        {
            this._pageService = pageService;
        }

        public ActionResult Index(string alias)
        {
            var pageModel = _pageService.GetPageByAlias(alias);
            var pageViewModel = Mapper.Map<Page, PageViewModel>(pageModel);
            return View(pageViewModel);
        }
    }
}