using AutoMapper;
using BotDetect.Web.Mvc;
using OnlineShop.Common;
using OnlineShop.Model.Model;
using OnlineShop.Service;
using OnlineShop.Web.Infrastructure.Extensions;
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
        IFeedbackService _feedbackService;
        FeedbackViewModel feedbackVm;

        public ContactController(IContactDetailsService contactDetailService, IFeedbackService feedbackService)
        {
            this._contactDetailService = contactDetailService;
            this._feedbackService = feedbackService;
            feedbackVm = new FeedbackViewModel();
        }

        public ActionResult Index()

        {
            feedbackVm.ContactDetailVm = GetContact();
            return View(feedbackVm);
        }

        private ContactDetailViewModel GetContact()
        {
            var contactDetail = _contactDetailService.GetContact();
            return Mapper.Map<ContactDetail, ContactDetailViewModel>(contactDetail);
        }

        [HttpPost]
        [SimpleCaptchaValidation("CaptchaCode", "ExampleCaptcha", "Incorrect CAPTCHA code!")]
        public ActionResult SendFeedback(FeedbackViewModel feedbackVm)
        {
            if (ModelState.IsValid)
            {
                var feedback = new Feedback();
                feedback.UpdateFeedback(feedbackVm);

                _feedbackService.AddFeedback(feedback);
                _feedbackService.Save();
                TempData["SuccessMsg"] = true;

                var stringContent = System.IO.File.ReadAllText(Server.MapPath("~/Assets/Client/template/mailBody.html"));
                stringContent = stringContent.Replace("{{Name}}", feedbackVm.Name)
                                            .Replace("{{Email}}", feedbackVm.Email)
                                            .Replace("{{Message}}", feedbackVm.Content);

                var toEmail = ConfigHelper.GetValueByKey("toEmail");
                MailHelper.SendMail(toEmail, $"Customer: {feedback.Name} - ID: {feedback.ID} ", stringContent);
                return RedirectToAction("Index", "Contact");
            }
            else
            {
                feedbackVm.ContactDetailVm = GetContact();
                return View("Index", feedbackVm);
            }
        }
    }
}