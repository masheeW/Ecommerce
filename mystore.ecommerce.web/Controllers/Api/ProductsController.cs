﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace mystore.ecommerce.web.Controllers.Api
{
    using mystore.ecommerce.contracts.managers;
    using mystore.ecommerce.dbcontext.Models;

    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _productManager.GetAllProducts();

                if (!results.HasError)
                {
                    return Ok(results.Payload);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error Occurred");
            }
        }
    }
}
