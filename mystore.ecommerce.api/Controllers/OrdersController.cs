using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mystore.ecommerce.contracts.Repositories;
using mystore.ecommerce.dbcontext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mystore.ecommerce.api.Controllers
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
        
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] OrderDetail model)
        //{
        //    try
        //    {
        //        if(ModelState.IsValid)
        //        {
        //            //add mapper
        //            var newOrder = _mapper.Map<OrderDetail, dbcontext.Models.Order>(model);
        //            //OR
        //            //var newOrder = new dbcontext.Models.Order()
        //            //{
        //            //    Id = model.Id,
        //            //    OrderDate = model.OrderDate,
        //            //    OrderNumber = model.OrderNumber
        //            //};
        //            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
        //            newOrder.Customer = currentUser.Id;
        //            var order = _orderRepository.AddOrder(newOrder);
        //            //map back to model
        //            return Created($"/api/orders/{order.Id}", _mapper.Map<dbcontext.Models.Order,OrderDetail>(order)); //use model instead Order
        //        }
        //        else
        //        {
        //            return BadRequest(ModelState);
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        return BadRequest("Failed to save");
        //    }
        //}
    }
}
