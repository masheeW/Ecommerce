using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mystore.ecommerce.contracts.managers;
using mystore.ecommerce.entities.Models;
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

        public async Task<IActionResult> Index()
        {
            var products = await _productManager.GetAllProducts();
            if (!products.HasError)
            {
                return View(products.Payload);
            }
            return View(null);
        }

        public virtual ActionResult Add()
        {
            ViewBag.Categories = _productManager.GetProductCategories().Payload;
            return View(new ProductModel());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public virtual ActionResult Add(ProductModel productModel, IFormFile postedFile)
        {
            try
            {
                ViewBag.Categories = _productManager.GetProductCategories().Payload;
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

                productModel.Id = Guid.NewGuid().ToString();
                productModel.ImageName = postedFile.FileName;

                var savedProduct = _productManager.SaveProduct(productModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View(productModel);
            }
            
            return View();
        }

        public virtual ActionResult Edit(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = _productManager.GetProductById(id);

                if (response != null)
                {
                    ViewBag.Categories = _productManager.GetProductCategories().Payload;

                    return View(response.Payload);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public virtual ActionResult Edit(ProductModel productViewModel)
        {
            ViewBag.Categories = _productManager.GetProductCategories().Payload;
            _productManager.UpdateProduct(productViewModel);
            return View(productViewModel);
        }

    }
}
