using Microsoft.AspNet.Identity.Owin;
using OnlineShop.Service;
using OnlineShop.Web.App_Start;
using OnlineShop.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace OnlineShop.Web.API
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiControllerBase
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController(IErrorService errorService, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(errorService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
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
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<HttpResponseMessage> Login(HttpRequestMessage request, string username, string password, bool rememberMe)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(username, password, rememberMe, shouldLockout: false);
                return request.CreateResponse(HttpStatusCode.OK, result);
            }
            return request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPost]
        [Authorize]
        [Route("logout")]
        public HttpResponseMessage Logout(HttpRequestMessage request)
        {
            var authenticationmanager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationmanager.SignOut();
            return request.CreateResponse(HttpStatusCode.OK, new { success = true });
        }
    }
}