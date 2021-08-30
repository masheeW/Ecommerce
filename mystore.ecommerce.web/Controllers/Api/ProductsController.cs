using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mystore.ecommerce.contracts.Repositories;
using mystore.ecommerce.dbcontext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mystore.ecommerce.web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productsRepository;
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;
        UserManager<StoreUser> _userManager;

        public ProductsController(IProductRepository productsRepository, ILogger<ProductsController> logger, IMapper mapper, UserManager<StoreUser> userManager)
        {
            _productsRepository = productsRepository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = _productsRepository.GetAllProducts();

                return Ok(results);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error Occurred");
            }
        }
    }
}
