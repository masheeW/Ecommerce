using mystore.ecommerce.dbcontext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mystore.ecommerce.contracts.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(string Id);
        Order AddOrder(Order order);
        IEnumerable<Order> GetAllOrdersByUser(string username);
        void AddEntity(object entity);
        bool SaveAll();
    }
}
