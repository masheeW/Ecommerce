using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace mystore.ecommerce.web.Controllers
{
    public class AppController : Controller
    {
        private readonly ILogger<AppController> _logger;

        public AppController(ILogger<AppController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {          
            return View();            
        }

    }
}
