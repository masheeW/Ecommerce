using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mystore.ecommerce.utility.common
{
    public class ServiceResponse<T>
    {
        public ServiceResponse(T t)
        {
            Payload = t;
        }

        public readonly T Payload;
        public string ErrorMessage { get; set; }
        public bool HasError { get; set; }
    }
}
