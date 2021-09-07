using AutoMapper;
using Microsoft.Extensions.Logging;
using mystore.ecommerce.contracts.managers;
using mystore.ecommerce.contracts.mappers;
using mystore.ecommerce.contracts.Repositories;
using mystore.ecommerce.dbcontext.Models;
using mystore.ecommerce.entities.Models;
using mystore.ecommerce.utility.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<ServiceResponse<IEnumerable<ProductModel>>> GetProducts()
        {
            try
            {
                var productList = await _productRepository.GetProducts();
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

        public async Task<ServiceResponse<IEnumerable<ProductModel>>> GetAvailableProducts()
        {
            try
            {
                var productList = await _productRepository.GetAllAvailableProducts();
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

        public async Task<ServiceResponse<IEnumerable<ProductModel>>> GetSearchedProducts(SearchRequest search)
        {
            try
            {
                var productList = await _productRepository.GetAllAvailableProductsBySearch(search.Category, search.Name);
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

                productMapper.CreatedBy = Constants.UserRole.Admin;
                productMapper.CreatedDate = DateTime.Now;
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
        public async Task<ServiceResponse<IEnumerable<ProductCategory>>> GetProductCategories()
        {
            try
            {
                var productCategory = await _productRepository.GetProductCategoriesAsync();

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
                    var productModel = _listMapper.Map<Product, ProductModel>(product);
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
                productMapper.UpdatedBy = Constants.UserRole.Admin;
                productMapper.UpdatedDate = DateTime.Now;
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
