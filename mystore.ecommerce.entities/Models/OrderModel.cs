using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mystore.ecommerce.entities.Models
{
    public class OrderModel
    {
        public string OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        [Required]
        [MinLength(4)]
        public string OrderNumber { get; set; }

        public ICollection<OrderItemModel> OrderItems { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }

    }
}
