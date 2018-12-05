using BotDetect.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using OnlineShop.Common;
using OnlineShop.Model.Model;
using OnlineShop.Web.App_Start;
using OnlineShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Web.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager;
        public AccountController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [SimpleCaptchaValidation("CaptchaCode", "ExampleCaptcha", "Incorrect CAPTCHA code!")]
        public async Task<ActionResult> Register(RegisterViewModel registerVm)
        {
            if (ModelState.IsValid)
            {
                var userEmail = await _userManager.FindByEmailAsync(registerVm.Email);
                if (userEmail != null)
                {
                    ModelState.AddModelError("Error", "Email is existed");
                    return View(registerVm);
                }
                var userName = await _userManager.FindByNameAsync(registerVm.Username);
                if (userName != null)
                {
                    ModelState.AddModelError("Error", "Email is existed");
                    return View(registerVm);
                }

                var user = new ApplicationUser()
                {
                    UserName = registerVm.Username,
                    Email = registerVm.Email,
                    EmailConfirmed = true,
                    Fullname = registerVm.Firstname + registerVm.Lastname,
                    PhoneNumber = registerVm.Phone,
                    Address = registerVm.Address,
                };

                await _userManager.CreateAsync(user, registerVm.Password);
                var adminUser = await _userManager.FindByEmailAsync(registerVm.Email);
                if (adminUser != null)
                    await _userManager.AddToRolesAsync(adminUser.Id, new string[] { "User" });

                var stringContent = System.IO.File.ReadAllText(Server.MapPath("~/Assets/Client/template/customerNotice.html"));
                stringContent = stringContent.Replace("{{Name}}", registerVm.Username)
                                            .Replace("{{Date}}", DateTime.Now.ToShortDateString())
                                            .Replace("{{Fullname}}", $"{registerVm.Firstname} {registerVm.Lastname}");

                var toEmail = ConfigHelper.GetValueByKey("toEmail");
                MailHelper.SendMail(toEmail, $"*** Register Succeeded ***", stringContent);
            }
            else
            {
                return View();
            }
            return RedirectToAction("Success", "Account", new { username = registerVm.Username });
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}