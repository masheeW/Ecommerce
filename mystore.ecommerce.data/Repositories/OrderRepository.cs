using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using mystore.ecommerce.contracts.Repositories;
using mystore.ecommerce.dbcontext;
using mystore.ecommerce.dbcontext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mystore.ecommerce.data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EcommerceDbContext _context;

        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(EcommerceDbContext context, ILogger<OrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        public IEnumerable<Order> GetAllOrders()
        {
            try
            {
                return _context.Orders.Include(o => o.Items).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return null;
            }
        }

        public Order GetOrderById(int Id)
        {
            try
            {
                return _context.Orders
                    .Include(o => o.Items)
                    .Where(o => o.Id == Id)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return null;
            }
        }

        public Order AddOrder(Order order)
        {
            var savedOrder =_context.Add(order);
            _context.SaveChanges();
            return savedOrder.Entity;
        }
    }
}
