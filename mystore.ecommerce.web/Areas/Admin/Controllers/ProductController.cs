using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mystore.ecommerce.contracts.managers;
using mystore.ecommerce.entities.Models;
using mystore.ecommerce.web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace mystore.ecommerce.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private IHostingEnvironment Environment;
        private readonly ILogger<ProductController> _logger;
        private readonly IProductManager _productManager;

        public ProductController(IHostingEnvironment _environment, ILogger<ProductController> logger, IProductManager productManager)
        {
            Environment = _environment;
            _logger = logger;
            _productManager = productManager;
        }

        public IActionResult Index()
        {
            try
            {
                var model = new ProductViewModel();
                model.Products = _productManager.GetAllProducts();

                ViewBag.Categories = _productManager.GetProductCategories();

                return View(model);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }


        [HttpPost]
        public IActionResult Index(ProductViewModel productModel, IFormFile postedFile)
        {
            try
            {
                string wwwPath = this.Environment.WebRootPath;
                string contentPath = this.Environment.ContentRootPath;

                string path = Path.Combine(this.Environment.WebRootPath, "img");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (postedFile != null)
                {
                    string fileName = Path.GetFileName(postedFile.FileName);
                    using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                        ViewBag.Message = fileName;
                    }
                }

                var newProduct = new ProductModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductName = productModel.ProductName,
                    Category = productModel.Category,
                    Price = productModel.Price,
                    Size = productModel.Size,
                    ImageName = postedFile.FileName
                };

                _productManager.SaveProduct(newProduct);
                
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View(productModel);
            }

            ViewBag.Categories = _productManager.GetProductCategories();
            return Index();
        }

    }
}
