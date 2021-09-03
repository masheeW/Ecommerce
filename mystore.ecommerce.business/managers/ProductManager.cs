using AutoMapper;
using Microsoft.Extensions.Logging;
using mystore.ecommerce.contracts.managers;
using mystore.ecommerce.contracts.mappers;
using mystore.ecommerce.contracts.Repositories;
using mystore.ecommerce.dbcontext.Models;
using mystore.ecommerce.entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mystore.ecommerce.business.managers
{
    public class ProductManager : IProductManager
    {
        private readonly ILogger<ProductManager> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IMapper<ProductModel, Product> _mapper;
        private readonly IMapper _listMapper;

        public ProductManager(ILogger<ProductManager> logger, IProductRepository productRepository, IMapper<ProductModel, Product> mapper, IMapper listMapper)
        {
            _logger = logger;
            _productRepository = productRepository;
            _mapper = mapper;
            _listMapper = listMapper;
        }

        public IEnumerable<ProductModel> GetAllProducts()
        {
            try
            {
                return _listMapper.Map<IEnumerable<Product>,IEnumerable<ProductModel>>(_productRepository.GetAllProducts());
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
                var saveObj = _mapper.Map(product);
                
                _productRepository.AddEntity(saveObj);
                _productRepository.SaveAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }


        public IEnumerable<ProductCategory> GetProductCategories()
        {
            try
            {
                return _productRepository.GetProductCategories();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
         
}
