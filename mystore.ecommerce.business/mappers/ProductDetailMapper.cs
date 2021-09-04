using mystore.ecommerce.contracts.mappers;
using mystore.ecommerce.contracts.Repositories;
using mystore.ecommerce.dbcontext.Models;
using mystore.ecommerce.entities.Models;
using mystore.ecommerce.utility.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mystore.ecommerce.business.mappers
{
    public class ProductDetailMapper : IMapper<ProductModel, Product>
    {
        private readonly IProductRepository _productRepository;

        public ProductDetailMapper(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product Map(ProductModel product)
        {
            var categoryNavigation = _productRepository.GetProductCategories().Where(p => p.Id == product.Category).FirstOrDefault();
            return new Product()
            {
                Id = product.Id,
                Category = product.Category,
                CategoryNavigation = categoryNavigation,
                CreatedBy = Constants.UserRole.Admin,
                CreatedDate = DateTime.Now,
                ImageName = product.ImageName,
                ProductName = product.ProductName,
                Price = product.Price,
                Size = product.Size,                
        };

        }
    }
}
