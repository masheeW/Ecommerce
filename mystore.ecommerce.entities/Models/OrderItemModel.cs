using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mystore.ecommerce.entities.Models
{
    public class OrderItemModel
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        [Required]
        public string ProductId { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Size { get; set; }
        public string ImageName { get; set; }

        public ProductModel Product { get; set; }
    }
}
