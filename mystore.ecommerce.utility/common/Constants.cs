using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mystore.ecommerce.utility.common
{
    public static class Constants
    {     

        public static class UserRole
        {
            public const string Admin = "Admin";
            public const string Customer = "Customer";
        }

        public static class OrderStatus
        {
            public const string New = "New";
            public const string Processing = "Processing";
            public const string Delivered = "Delivered";
            public const string Cancelled = "Cancelled";
        }

    }
}
