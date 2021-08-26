using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mystore.ecommerce.entities.Order
{
    public class OrderItemDetail
    {
        public int Id { get; set; }
        public string ItemName { get; set; }

        public int Quantity { get; set; }
        public OrderDetail Order { get; set; }
    }
}
