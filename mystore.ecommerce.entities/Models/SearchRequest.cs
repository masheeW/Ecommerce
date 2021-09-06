using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mystore.ecommerce.entities.Models
{
    public class SearchRequest
    {
        public string Category { get; set; }
        public string Name { get; set; }
    }
}
