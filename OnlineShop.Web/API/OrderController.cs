using AutoMapper;
using OnlineShop.Common;
using OnlineShop.Model.Model;
using OnlineShop.Service;
using OnlineShop.Web.Infrastructure.Core;
using OnlineShop.Web.Infrastructure.Extensions;
using OnlineShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShop.Web.API
{
    [RoutePrefix("api/order")]
    public class OrderController : ApiControllerBase
    {
        IOrderService _orderService;
        IProductService _productService;

        public OrderController(IErrorService errorService, IOrderService orderService, IProductService productService) : base(errorService)
        {
            this._orderService = orderService;
            this._productService = productService;
        }

        [Route("get_customer_orders")]
        [HttpGet]
        public HttpResponseMessage GetCustomerOrders(HttpRequestMessage request)
        {
            return CreateHttpReponse(request, () =>
            {
                var listOrder = _orderService.GetAllOrder();
                foreach (var order in listOrder)
                {
                    var listOrderDetailById = _orderService.GetListOrderDetailsByOrderId(order.ID);
                    order.OrderDetails = listOrderDetailById;
                    foreach (var oderDetail in listOrderDetailById)
                    {
                        order.ProductQuantity += oderDetail.Quantity.Value;
                    }
                }

                var listOrderViewModel = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(listOrder);
                return request.CreateResponse(HttpStatusCode.OK, listOrderViewModel);
            });
        }

        [Route("load_order/{id:int}")]
        [HttpGet]
        public HttpResponseMessage LoadOrderDetail(HttpRequestMessage request, int id)
        {
            return CreateHttpReponse(request, () =>
            {
                var orderDetails = _orderService.GetListOrderDetailsByOrderId(id);
                var orderModel = _orderService.GetSingleOrder(id);
                var OrderViewModel = Mapper.Map<Order, OrderViewModel>(orderModel);
                var orderDetailsViewModel = Mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailViewModel>>(orderDetails);
                foreach (var orderDetail in orderDetailsViewModel)
                {
                    orderDetail.Product = Mapper.Map<Product, ProductViewModel>(_productService.GetSingleProduct(orderDetail.ProductID));
                    orderDetail.ProductUrl = $"/{orderDetail.Product.Alias}.p-{orderDetail.Product.ID}.html";
                    OrderViewModel.ProductQuantity += orderDetail.Quantity.Value;
                }
                OrderViewModel.OrderDetails = orderDetailsViewModel;

                return request.CreateResponse(HttpStatusCode.OK, OrderViewModel);
            });
        }

        [Route("print_order")]
        [HttpPost]
        public HttpResponseMessage PrintOrder(HttpRequestMessage request, OrderViewModel orderVm)
        {
            if (ModelState.IsValid)
            {
                Func<HttpResponseMessage> func = delegate ()
                {
                    var orderModel = _orderService.GetSingleOrder(orderVm.ID);

                    try
                    {
                        MailHelper.SendMail(orderModel.CustomerEmail, "** Đơn hàng của bạn đang được vận chuyển **", orderVm.CustomerMessage);
                    }
                    catch (Exception ex)
                    {
                        return request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                    }
                    orderModel.Status = true;
                    _orderService.UpdateOrder(orderModel);
                    _orderService.Save();
                    var orderViewModel = Mapper.Map<Order, OrderViewModel>(orderModel);
                    return request.CreateResponse(HttpStatusCode.OK, orderViewModel);
                };
                return CreateHttpReponse(request, func);
            }
            return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }
    }
}
