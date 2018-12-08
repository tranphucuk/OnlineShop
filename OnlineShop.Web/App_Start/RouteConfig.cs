using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}",
            new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
                name: "Register",
                url: "register.html",
                defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional },
                namespaces: new string[] { "OnlineShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Register Succeeded",
                url: "register-succeeded/{username}.html",
                defaults: new { controller = "Account", action = "Success", username = UrlParameter.Optional },
                namespaces: new string[] { "OnlineShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Shopping Cart",
                url: "yourcart.html",
                defaults: new { controller = "ShoppingCart", action = "Index", id = UrlParameter.Optional },    
                namespaces: new string[] { "OnlineShop.Web.Controllers" }
            );

            routes.MapRoute(
               name: "Checkout",
               url: "checkout.html",
               defaults: new { controller = "ShoppingCart", action = "Checkout", id = UrlParameter.Optional },
               namespaces: new string[] { "OnlineShop.Web.Controllers" }
           );

            routes.MapRoute(
                name: "Login",
                url: "login.html",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new string[] { "OnlineShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Contact Details",
                url: "contact-details.html",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "OnlineShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Page Redirect",
                url: "page/{alias}.html",
                defaults: new { controller = "Page", action = "Index", alias = UrlParameter.Optional },
                namespaces: new string[] { "OnlineShop.Web.Controllers" }
            );

            routes.MapRoute(
               name: "Search Product",
               url: "search.html",
               defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
               namespaces: new string[] { "OnlineShop.Web.Controllers" }
           );

            routes.MapRoute(
                name: "Product Category",
                url: "{alias}.pc-{id}.html",
                defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
                namespaces: new string[] { "OnlineShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Product",
                url: "{alias}.p-{id}.html",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                namespaces: new string[] { "OnlineShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Products By Tag",
                url: "tag/{tagId}.html",
                defaults: new { controller = "Product", action = "ListProductsByTag", tagId = UrlParameter.Optional },
                namespaces: new string[] { "OnlineShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "OnlineShop.Web.Controllers" }
            );
        }
    }
}