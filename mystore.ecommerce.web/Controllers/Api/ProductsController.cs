using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mystore.ecommerce.contracts.managers;
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
  
    public class ProductsController : Controller
    {
        private readonly IProductManager _productManager;
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;
        UserManager<StoreUser> _userManager;

        public ProductsController(IProductManager productsManager, ILogger<ProductsController> logger, IMapper mapper, UserManager<StoreUser> userManager)
        {
            _productManager = productsManager;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }


        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _productManager.GetAvailableProducts();

                return Ok(results.Payload);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error Occurred");
            }
        }


        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> Post([FromBody] SearchRequest request)
        {
            try
            {
                var results = await _productManager.GetSearchedProducts(request);

                return Ok(results.Payload);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error Occurred");
            }
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var results = await _productManager.GetProductCategories();

                return Ok(results.Payload);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error Occurred");
            }
        }
    }
}
