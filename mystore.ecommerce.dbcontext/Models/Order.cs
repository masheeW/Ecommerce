﻿
using System;
using System.Collections.Generic;


namespace mystore.ecommerce.dbcontext.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public StoreUser  User { get; set; }
        public ICollection<OrderItem> Items { get; set; }
    }
}