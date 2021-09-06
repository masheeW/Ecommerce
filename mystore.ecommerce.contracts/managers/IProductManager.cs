using mystore.ecommerce.dbcontext.Models;
using mystore.ecommerce.entities.Models;
using mystore.ecommerce.utility.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mystore.ecommerce.contracts.managers
{
    public interface IProductManager
    {
        Task<ServiceResponse<IEnumerable<ProductModel>>> GetProducts();
        Task<ServiceResponse<IEnumerable<ProductModel>>> GetAvailableProducts();
        Task<ServiceResponse<IEnumerable<ProductModel>>> GetSearchedProducts(SearchRequest search);
        ServiceResponse<ProductModel> GetProductById(string id);
        ServiceResponse<IEnumerable<ProductCategory>> GetProductCategories();
        ServiceResponse<ProductModel> SaveProduct(ProductModel product);
        ServiceResponse<ProductModel> UpdateProduct(ProductModel product);
    }
}
