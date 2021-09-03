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
        IEnumerable<Product> GetAllProducts();
        IEnumerable<ProductCategory> GetProductCategories();
        IEnumerable<Product> GetProductsByCategory(string categpry);
        void AddEntity(object entity);
        bool SaveAll();
    }
}
