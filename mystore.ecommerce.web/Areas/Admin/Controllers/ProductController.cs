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
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class ProductController : Controller
    {
        private IWebHostEnvironment Environment;
        private readonly ILogger<ProductController> _logger;
        private readonly IProductManager _productManager;

        public ProductController(IWebHostEnvironment _environment, ILogger<ProductController> logger, IProductManager productManager)
        {
            Environment = _environment;
            _logger = logger;
            _productManager = productManager;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productManager.GetProducts();
            if (!products.HasError)
            {
                return View(products.Payload);
            }
            return View(null);
        }

        public virtual async Task<ActionResult> Add()
        {
            var response = await _productManager.GetProductCategories();
            ViewBag.Categories = response.Payload;
            return View(new ProductModel());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public virtual async Task<ActionResult> Add(ProductModel productModel, IFormFile postedFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _productManager.GetProductCategories();
                    ViewBag.Categories = response.Payload;
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
                    else
                    {
                        ModelState.AddModelError("", "Please add a product image");
                        return View(productModel);
                    }

                    productModel.Id = Guid.NewGuid().ToString();
                    productModel.ImageName = postedFile.FileName;

                    var savedProduct = _productManager.SaveProduct(productModel);
                    if (!savedProduct.HasError)
                    {
                        TempData["Message"] = "Product successfully added to the system ";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", savedProduct.ErrorMessage);
                    }

                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Save failed");
                _logger.LogError(ex.Message);
            }

            return View(productModel);
        }

        public virtual async Task<ActionResult> Edit(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = _productManager.GetProductById(id);

                if (response != null)
                {
                    var categoryResponse = await _productManager.GetProductCategories();
                    ViewBag.Categories = categoryResponse.Payload;

                    return View(response.Payload);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public virtual async Task<ActionResult> Edit(ProductModel productViewModel)
        {
            var categories = await _productManager.GetProductCategories();
            ViewBag.Categories = categories.Payload;
            var response = _productManager.UpdateProduct(productViewModel);
            if (!response.HasError)
            {
                TempData["Message"] = "Product successfully updated";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", response.ErrorMessage);
            }
            return View(productViewModel);
        }


    }
}
