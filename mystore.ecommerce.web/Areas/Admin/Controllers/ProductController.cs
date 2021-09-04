using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace mystore.ecommerce.web.Areas.Admin.Controllers
{
    using mystore.ecommerce.contracts.managers;
    using mystore.ecommerce.entities.Models;
    using mystore.ecommerce.web.Areas.Admin.Models;
    using System.Threading.Tasks;

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
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

                var savedProduct = _productManager.SaveProduct(newProduct);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View(productModel);
            }

            return View(productModel);
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
        public virtual ActionResult Edit(ProductModel productModel, IFormFile postedFile)
        {
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
            productModel.ImageName = postedFile.FileName;

            var savedProduct = _productManager.UpdateProduct(productModel);

            return View(productModel);
        }

    }
}
