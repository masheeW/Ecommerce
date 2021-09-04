
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mystore.ecommerce.entities.Models
{
    public class ProductModel
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public string ImageName { get; set; }

    }
}
