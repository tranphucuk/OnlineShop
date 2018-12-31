using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Service
{
    public interface IOrderService
    {
        bool CreateOrder(Order order);
        IEnumerable<OrderDetail> GetAllOrderDetails();
        IEnumerable<Order> GetAllOrder();
        IEnumerable<OrderDetail> GetListOrderDetailsByOrderId(int orderId);
        Order GetSingleOrder(int id);
        void UpdateOrder(Order order);
        void Save();
    }

    public class OrderService : IOrderService
    {
        IOrderRepository _orderRepository;
        IUnitOfWork _unitOfWork;
        IOrderDetailRepository _orderDetailRepository;

        public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IOrderDetailRepository orderDetailRepository)
        {
            this._orderRepository = orderRepository;
            this._orderDetailRepository = orderDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        public bool CreateOrder(Order order)
        {
            try
            {
                _orderRepository.Add(order);
                _unitOfWork.Commit();

                foreach (var item in order.OrderDetails)
                {
                    item.OrderID = order.ID;
                    _orderDetailRepository.Add(item);
                }
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<Order> GetAllOrder()
        {
            return _orderRepository.GetAll();
        }

        public IEnumerable<OrderDetail> GetAllOrderDetails()
        {
            return _orderDetailRepository.GetAll();
        }

        public IEnumerable<OrderDetail> GetListOrderDetailsByOrderId(int orderId)
        {
            return _orderRepository.GetListOrderDetails(orderId);
        }

        public Order GetSingleOrder(int id)
        {
            return _orderRepository.GetSingleEntity(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void UpdateOrder(Order order)
        {
            _orderRepository.Update(order);
        }
    }
}
