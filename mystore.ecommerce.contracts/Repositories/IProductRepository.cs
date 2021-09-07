using mystore.ecommerce.dbcontext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mystore.ecommerce.contracts.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product>> GetAllAvailableProducts();
        Task<IEnumerable<Product>> GetAllAvailableProductsBySearch(string categoryId, string text);
        IEnumerable<Product> GetProductsByCategory(string categpry);
        Product AddProduct(Product product);
        bool SaveAll();
        Product GetProductById(string id);
        Product UpdateProduct(Product productMapper);
        Task<IEnumerable<ProductCategory>> GetProductCategoriesAsync();
        IEnumerable<ProductCategory> GetProductCategories();
    }
}
