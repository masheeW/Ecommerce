using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mystore.ecommerce.contracts.Repositories;
using mystore.ecommerce.entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mystore.ecommerce.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;
        public OrdersController(IOrderRepository orderRepository, ILogger<OrdersController> logger, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_orderRepository.GetAllOrders());
                
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error Occurred");
            }
        }


        [HttpGet("{id:int}")]
        public IActionResult Get(int Id)
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
        public IActionResult Post([FromBody] OrderDetail model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    //add mapper
                    var newOrder = _mapper.Map<OrderDetail, dbcontext.Models.Order>(model);
                    //OR
                    //var newOrder = new dbcontext.Models.Order()
                    //{
                    //    Id = model.Id,
                    //    OrderDate = model.OrderDate,
                    //    OrderNumber = model.OrderNumber
                    //};
                    var order = _orderRepository.AddOrder(newOrder);
                    //map back to model
                    return Created($"/api/orders/{order.Id}", _mapper.Map<dbcontext.Models.Order,OrderDetail>(order)); //use model instead Order
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Failed to save");
            }
        }
    }
}
