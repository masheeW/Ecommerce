using AutoMapper;
using Microsoft.Extensions.Logging;
using mystore.ecommerce.contracts.managers;
using mystore.ecommerce.contracts.Repositories;
using mystore.ecommerce.dbcontext.Models;
using mystore.ecommerce.entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mystore.ecommerce.business.managers
{
    public class ProductManager : IProductManager
    {
        private readonly ILogger<ProductManager> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductManager(ILogger<ProductManager> logger, IProductRepository productRepository, IMapper mapper)
        {
            _logger = logger;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public IEnumerable<ProductModel> GetAllProducts()
        {
            try
            {
                return  _mapper.Map <IEnumerable<Product>, IEnumerable<ProductModel>>(_productRepository.GetAllProducts());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public void SaveProduct(ProductModel product)
        {
            try
            {
                var saveObj = _mapper.Map<ProductModel, Product>(product);
                saveObj.CreatedBy = "Admin";
                saveObj.CreatedDate = DateTime.Now;
                _productRepository.AddEntity(saveObj);
                _productRepository.SaveAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
         
}
