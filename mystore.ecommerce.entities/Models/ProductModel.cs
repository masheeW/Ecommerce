
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mystore.ecommerce.entities.Models
{
    public class ProductModel
    {
        public string Id { get; set; }
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

        public bool Status { get; set; }
        public bool OutOfStock { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
