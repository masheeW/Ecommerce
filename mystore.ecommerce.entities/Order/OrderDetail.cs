using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mystore.ecommerce.entities.Order
{
    public class OrderDetail
    {        
        public int Id { get; set; }
        [Required]
        [MinLength(4)]
        public string OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }

        public ICollection<OrderItemDetail> Items { get; set; }
    }
}
