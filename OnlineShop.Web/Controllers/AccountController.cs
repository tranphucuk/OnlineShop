using BotDetect.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OnlineShop.Common;
using OnlineShop.Model.Model;
using OnlineShop.Web.App_Start;
using OnlineShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginVm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(loginVm.Username, loginVm.Password);
                if (user != null)
                {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    ClaimsIdentity identity = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationProperties props = new AuthenticationProperties();
                    props.IsPersistent = loginVm.RememberMe;
                    authenticationManager.SignIn(props, identity);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
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