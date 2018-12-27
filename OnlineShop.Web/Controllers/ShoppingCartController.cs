using AutoMapper;
using Microsoft.AspNet.Identity;
using OnlineShop.Common;
using OnlineShop.Model.Model;
using OnlineShop.Service;
using OnlineShop.Web.App_Start;
using OnlineShop.Web.Infrastructure.Extensions;
using OnlineShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OnlineShop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        IProductService _productService;
        ApplicationUserManager _userManager;
        IOrderService _orderService;

        public ShoppingCartController(IProductService productService, ApplicationUserManager userManager, IOrderService orderService)
        {
            this._productService = productService;
            this._userManager = userManager;
            this._orderService = orderService;
        }

        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            if (Session[CommonConstants.sessionCart] == null)
            {
                return Redirect("/yourcart.html");
            }
            return View();
        }

        [HttpPost]
        public JsonResult GetUserDetails()
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUserId = User.Identity.GetUserId();
                var currentUser = _userManager.FindById(currentUserId);

                return Json(new
                {
                    status = true,
                    data = currentUser,
                });
            }
            return Json(new
            {
                status = false,
            });
        }

        [HttpPost]
        public JsonResult Add(int productId)
        {
            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.sessionCart];
            if (cart == null) cart = new List<ShoppingCartViewModel>();
            if (cart.Any(x => x.ProductId == productId))
            {
                foreach (var item in cart)
                {
                    if (item.ProductId == productId)
                    {
                        item.Quantity += 1;
                    }
                }
            }
            else
            {
                ShoppingCartViewModel newItem = new ShoppingCartViewModel();
                newItem.ProductId = productId;
                var productModel = _productService.GetSingleProduct(productId);
                var productViewModel = Mapper.Map<Product, ProductViewModel>(productModel);

                newItem.Quantity = 1;
                newItem.Product = productViewModel;
                newItem.CartID = cart.Count + 1;
                cart.Add(newItem);
            }
            Session[CommonConstants.sessionCart] = cart;
            return Json(new
            {
                status = true,
                data = _productService.GetSingleProduct(productId).Name
            });
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.sessionCart];
            if (cart == null || cart.Count() == 0)
            {
                return Json(new
                {
                    status = false,
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                status = true,
                data = cart,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(string cartData)
        {
            try
            {
                var cartViewModel = new JavaScriptSerializer().Deserialize<List<ShoppingCartViewModel>>(cartData);
                var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.sessionCart];

                foreach (var item in cartSession)
                {
                    foreach (var jitem in cartViewModel)
                    {
                        if (item.CartID == jitem.CartID)
                        {
                            item.Quantity = jitem.Quantity;
                        }
                    }
                }
                Session[CommonConstants.sessionCart] = cartSession;
                return Json(new
                {
                    status = true,
                });
            }
            catch (Exception)
            {

                return Json(new
                {
                    status = false,
                });
            }
        }

        [HttpPost]
        public JsonResult DeleteItem(int cartId)
        {
            var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.sessionCart];
            cartSession.RemoveAll(x => x.CartID == cartId);
            return Json(new
            {
                status = true,
            });
        }

        [HttpPost]
        public JsonResult DeleteAll()
        {
            Session[CommonConstants.sessionCart] = new List<ShoppingCartViewModel>();
            return Json(new
            {
                status = true
            });
        }

        public JsonResult CreateOrder(string orderString)
        {
            var orderViewModel = new JavaScriptSerializer().Deserialize<OrderViewModel>(orderString);
            var order = new Order();
            order.UpdateOrder(orderViewModel);

            if (Request.IsAuthenticated)
            {
                order.CustomerId = User.Identity.GetUserId();
                order.CreatedBy = User.Identity.Name;
            }

            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.sessionCart];
            var listOrderDetail = new List<OrderDetail>();
            foreach (var item in cart)
            {
                var detail = new OrderDetail();
                detail.Quantity = item.Quantity;
                detail.ProductID = item.ProductId;
                listOrderDetail.Add(detail);
            }
            order.OrderDetails = listOrderDetail;

            var orderStatus = _orderService.CreateOrder(order);
            return Json(new
            {
                status = orderStatus
            });
        }
    }
}