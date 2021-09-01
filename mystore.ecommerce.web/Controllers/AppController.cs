using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mystore.ecommerce.contracts.Repositories;
using mystore.ecommerce.data;
using mystore.ecommerce.dbcontext;
using mystore.ecommerce.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mystore.ecommerce.web.Controllers
{
    public class AppController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<AppController> _logger;

        public AppController(IOrderRepository orderRepository, ILogger<AppController> logger, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _productRepository = productRepository;
        }
        public IActionResult Index()
        {
            var results = _productRepository.GetAllProducts();
            return View(results);
        }

        [HttpGet("contact")]
        public IActionResult ContactUs()
        {
            return View();
        }


        [HttpPost("contact")]
        public IActionResult ContactUs(ContactUsViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            else
            {
                return View();
            }
        }

      
        public IActionResult Shop()
        {
                var results = _productRepository.GetAllProducts();
                return View(results);
        }
    }
}
