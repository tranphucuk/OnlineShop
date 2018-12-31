using OnlineShop.Data.Infrastructure;
using OnlineShop.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<OrderDetail> GetListOrderDetails(int orderId);
    }

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<OrderDetail> GetListOrderDetails(int orderId)
        {
            var query = from o in DbContext.Orders
                        join od in DbContext.OrderDetails
                        on o.ID equals od.OrderID
                        where o.ID == orderId
                        select od;
            return query;
        }
    }
}
