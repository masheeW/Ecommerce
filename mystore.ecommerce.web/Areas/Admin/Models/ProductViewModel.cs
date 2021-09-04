using mystore.ecommerce.entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mystore.ecommerce.web.Areas.Admin.Models
{
    public class ProductViewModel
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Size { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid price")]
        public decimal Price { get; set; }
        
        public string ImageName { get; set; }

        public IEnumerable<ProductModel> Products { get; set; }
    }
}
