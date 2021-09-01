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
        IEnumerable<ProductModel> GetAllProducts();

        void SaveProduct(ProductModel product);
    }
}
