using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using mystore.ecommerce.contracts.Repositories;
using mystore.ecommerce.dbcontext;
using mystore.ecommerce.dbcontext.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mystore.ecommerce.data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EcommercedbContext _context;

        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(EcommercedbContext context, ILogger<OrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        public IEnumerable<Order> GetAllOrders()
        {
            try
            {
                return _context.Order.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return null;
            }
        }

        public Order GetOrderById(string Id)
        {
            try
            {
                return _context.Order
                    .Where(o => o.Id == Id).Include(o=>o.OrderItems).ThenInclude(p=>p.Product)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return null;
            }
        }

        public IEnumerable<Order> GetAllOrdersByUser(string username)
        {
            try
            {
                return _context.Order.Where(o => o.Customer == username).Include(p=>p.OrderItems).OrderByDescending(p=>p.CreatedDate).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return null;
            }
        }


        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public Order AddOrder(Order order)
        {
            try
            {
                _context.Add(order);
                _context.SaveChanges();
                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return null;
            }
        }
    }
}
