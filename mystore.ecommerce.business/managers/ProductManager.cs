using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mystore.ecommerce.business.managers
{
    using mystore.ecommerce.business.utility;
    using mystore.ecommerce.contracts.managers;
    using mystore.ecommerce.contracts.mappers;
    using mystore.ecommerce.contracts.Repositories;
    using mystore.ecommerce.dbcontext.Models;
    using mystore.ecommerce.entities.Models;
    using System.Threading.Tasks;

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

        public async Task<ServiceResponse<IEnumerable<ProductModel>>> GetAllProducts()
        {
            try
            {
                var productList = await _productRepository.GetAllProducts();
                var products = _listMapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(productList);

                return new ServiceResponse<IEnumerable<ProductModel>>(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new ServiceResponse<IEnumerable<ProductModel>>(null)
                {
                    HasError = true,
                    ErrorMessage = ErrorMessages.APPLICATION_ERROR
                };
            }
        }
        public ServiceResponse<ProductModel> SaveProduct(ProductModel product)
        {
            try
            {
                var productMapper = _mapper.Map(product);

                var response = _productRepository.AddProduct(productMapper);
                if (response != null)
                {
                    var savedProductModel = _listMapper.Map<Product, ProductModel>(response);
                    return new ServiceResponse<ProductModel>(savedProductModel);
                }

                return new ServiceResponse<ProductModel>(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ServiceResponse<ProductModel>(null)
                {
                    HasError = true,
                    ErrorMessage = ErrorMessages.APPLICATION_ERROR
                };
            }
        }
        public ServiceResponse<IEnumerable<ProductCategory>> GetProductCategories()
        {
            try
            {
                var productCategory = _productRepository.GetProductCategories();

                return new ServiceResponse<IEnumerable<ProductCategory>>(productCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new ServiceResponse<IEnumerable<ProductCategory>>(null)
                {
                    HasError = true,
                    ErrorMessage = ErrorMessages.SAVE_FAILED
                };
            }
        }
        public ServiceResponse<ProductModel> GetProductById(string id)
        {
            try
            {
                var product = _productRepository.GetProductById(id);
                if (product != null)
                {
                    var productModel= _listMapper.Map<Product, ProductModel>(product);
                    return new ServiceResponse<ProductModel>(productModel);
                }

                return new ServiceResponse<ProductModel>(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new ServiceResponse<ProductModel>(null)
                {
                    HasError = true,
                    ErrorMessage = ErrorMessages.SAVE_FAILED
                };
            }
        }
        public ServiceResponse<ProductModel> UpdateProduct(ProductModel product)
        {
            try
            {
                var productMapper = _mapper.Map(product);

                var response = _productRepository.UpdateProduct(productMapper);
                if (response != null)
                {
                    var savedProductModel = _listMapper.Map<Product, ProductModel>(response);
                    return new ServiceResponse<ProductModel>(savedProductModel);
                }

                return new ServiceResponse<ProductModel>(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ServiceResponse<ProductModel>(null)
                {
                    HasError = true,
                    ErrorMessage = ErrorMessages.APPLICATION_ERROR
                };
            }
        }
    }

}
