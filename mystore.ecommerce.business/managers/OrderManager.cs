using AutoMapper;
using Microsoft.Extensions.Logging;
using mystore.ecommerce.contracts.managers;
using mystore.ecommerce.contracts.Repositories;
using mystore.ecommerce.dbcontext.Models;
using mystore.ecommerce.entities.Models;
using mystore.ecommerce.utility.common;
using System;
using System.Collections.Generic;

namespace mystore.ecommerce.business.managers
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderManager> _logger;
        private readonly IMapper _mapper;

        public OrderManager(IOrderRepository orderRepository, ILogger<OrderManager> logger, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<Order> GetAllOrdersByUser(string userid)
        {
            try
            {
                
                return _orderRepository.GetAllOrdersByUser(userid);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }

        public Order GetOrderById(string orderId)
        {
            return _orderRepository.GetOrderById(orderId);
        }

        public Order SaveOrder(OrderModel order, StoreUser user)
        {
            try
            {
                var newOrder = _mapper.Map<Order>(order);

                newOrder.Id = Guid.NewGuid().ToString();
                newOrder.OrderItems = new List<OrderItem>();
                foreach (var item in order.OrderItems)
                {
                    var newItem = _mapper.Map<OrderItem>(item);
                    newItem.Id = Guid.NewGuid().ToString();
                    newItem.Order = newOrder;
                    newOrder.OrderItems.Add(newItem);
                }

                if (newOrder.OrderDate == DateTime.MinValue)
                {
                    newOrder.OrderDate = DateTime.Now;
                };
                             
                newOrder.Customer = user.Id;
                newOrder.CreatedBy = user.Id;
                newOrder.CreatedDate = DateTime.Now;
                newOrder.OrderStatus = Constants.OrderStatus.New;

                _orderRepository.AddOrder(newOrder);
                if (_orderRepository.SaveAll())
                {
                    return newOrder;
                }

                return newOrder;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
