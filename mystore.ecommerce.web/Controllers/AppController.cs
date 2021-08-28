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
        private readonly ILogger<AppController> _logger;

        public AppController(IOrderRepository orderRepository, ILogger<AppController> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
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

        [Authorize]
        public IActionResult Shop()
        {
            if (User.IsInRole("Customer"))
            {
                var results = _orderRepository.GetAllOrders();
                return View(results);
            }
            return RedirectToAction("Index", "App");
        }
    }
}
