using BotDetect.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OnlineShop.Common;
using OnlineShop.Model.Model;
using OnlineShop.Service;
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
        private ApplicationSignInManager _signInManager;
        private IApplicationGroupService _appGroupService;
        private IEmailService _emailService;
        public AccountController(ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            IApplicationGroupService appGroupService,
             IEmailService emailService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            this._appGroupService = appGroupService;
            this._emailService = emailService;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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
        public ActionResult Login()
        {
            if (Request.UrlReferrer != null)
            {
                ViewBag.returnUrl = Request.UrlReferrer.ToString();
            }
            else
            {
                ViewBag.returnUrl = Request.RawUrl;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    if (returnUrl != null)
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

        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: true);
            switch (result)
            {
                case SignInStatus.Success:
                    return Redirect(returnUrl);
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.returnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { returnUrl = returnUrl }));
        }

        [HttpPost]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    // add emails to email table for newsletter purpose
                    _emailService.Add(new Email()
                    {
                        CreatedDate = DateTime.Now,
                        EmailAddress = user.Email,
                        Status = true,
                    });
                    _emailService.Save();

                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        // [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
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
        [ValidateAntiForgeryToken]
        [SimpleCaptchaValidation("CaptchaCode", "ExampleCaptcha", "Incorrect CAPTCHA code!")]
        public async Task<ActionResult> Register(RegisterViewModel registerVm)
        {
            if (ModelState.IsValid)
            {
                var userEmail = await _userManager.FindByEmailAsync(registerVm.Email);
                if (userEmail != null)
                {
                    ModelState.AddModelError("Email", "Email is existed");
                    return View(registerVm);
                }
                var userName = await _userManager.FindByNameAsync(registerVm.Username);
                if (userName != null)
                {
                    ModelState.AddModelError("Username", "Username is existed");
                    return View(registerVm);
                }

                var user = new ApplicationUser()
                {
                    UserName = registerVm.Username,
                    Email = registerVm.Email,
                    EmailConfirmed = true,
                    Fullname = registerVm.Fullname,
                    PhoneNumber = registerVm.Phone,
                    Address = registerVm.Address,
                };

                await _userManager.CreateAsync(user, registerVm.Password);
                // add email to email table for newsletter purpose
                _emailService.Add(new Email()
                {
                    CreatedDate = DateTime.Now,
                    EmailAddress = registerVm.Email,
                    Status = true
                });
                _emailService.Save();

                var newUser = await _userManager.FindByEmailAsync(registerVm.Email);
                if (newUser != null)
                {
                    _appGroupService.AddUserToGroup(CommonConstants.user, newUser.Id);
                    _appGroupService.Save();
                }

                var stringContent = System.IO.File.ReadAllText(Server.MapPath("~/Assets/Client/template/customerNotice.html"));
                stringContent = stringContent.Replace("{{Name}}", registerVm.Username)
                                            .Replace("{{Date}}", DateTime.Now.ToShortDateString())
                                            .Replace("{{Fullname}}", registerVm.Fullname);

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

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}