using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mystore.ecommerce.contracts.Repositories;
using mystore.ecommerce.dbcontext.Models;
using mystore.ecommerce.entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mystore.ecommerce.web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;
        UserManager<StoreUser> _userManager;

        public OrdersController(IOrderRepository orderRepository, ILogger<OrdersController> logger, IMapper mapper, UserManager<StoreUser> userManager)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                var results = _orderRepository.GetAllOrdersByUser(currentUser.Id);
                
                return Ok(results);
                
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error Occurred");
            }
        }


        [HttpGet("{id}")]
        public IActionResult Get(string Id)
        {
            try
            {
                var order = _orderRepository.GetOrderById(Id);

                if(order != null)
                {
                    return Ok(order);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error Occurred");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = _mapper.Map<Order>(model);
                 
                    newOrder.Id = Guid.NewGuid().ToString();

                    foreach(var item in model.OrderItems)
                    {
                        var newItem = _mapper.Map<OrderItem>(item);
                        newItem.Id = Guid.NewGuid().ToString();
                        newItem.Order = newOrder;
                        newOrder.OrderItem.Add(newItem);
                    }

                    if (newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    };

                    var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    newOrder.Customer = currentUser.Id;
                    newOrder.CreatedBy = currentUser.Id;
                    newOrder.CreatedDate = DateTime.Now;
                    newOrder.OrderStatus = "Created";

                   _orderRepository.AddEntity(newOrder);
                    if (_orderRepository.SaveAll())
                    {
                        return Created($"/api/orders/{newOrder.Id}", _mapper.Map<OrderModel>(newOrder));
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save new order: {ex}");
            }

            return BadRequest("Failed to save new order.");
        }
    }
}
