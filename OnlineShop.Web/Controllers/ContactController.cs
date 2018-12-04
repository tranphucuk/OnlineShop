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
    public class ContactController : Controller
    {
        // GET: Contact
        IContactDetailsService _contactDetailService;

        public ContactController(IContactDetailsService contactDetailService)
        {
            this._contactDetailService = contactDetailService;
        }

        public ActionResult Index()
        {
            var contactDetail = _contactDetailService.GetContact();
            var contactDetalViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(contactDetail);
            return View(contactDetalViewModel);
        }
    }
}