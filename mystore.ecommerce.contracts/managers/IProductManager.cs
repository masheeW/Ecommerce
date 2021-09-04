using mystore.ecommerce.business.utility;
using mystore.ecommerce.dbcontext.Models;
using mystore.ecommerce.entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace mystore.ecommerce.contracts.managers
{
    public interface IProductManager
    {
        Task<ServiceResponse<IEnumerable<ProductModel>>> GetAllProducts();
        ServiceResponse<ProductModel> GetProductById(string id);
        ServiceResponse<IEnumerable<ProductCategory>> GetProductCategories();
        ServiceResponse<ProductModel> SaveProduct(ProductModel product);
        ServiceResponse<ProductModel> UpdateProduct(ProductModel product);
    }
}
