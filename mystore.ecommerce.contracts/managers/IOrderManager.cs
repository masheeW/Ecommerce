using mystore.ecommerce.dbcontext.Models;
using mystore.ecommerce.entities.Models;
using System.Collections.Generic;

namespace mystore.ecommerce.contracts.managers
{
    public interface IOrderManager
    {
        IEnumerable<Order> GetAllOrdersByUser(string userid);
        Order GetOrderById(string orderId);
        Order SaveOrder(OrderModel order, StoreUser user);
    }
}
