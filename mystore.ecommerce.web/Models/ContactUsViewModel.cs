﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mystore.ecommerce.web.Models
{
    public class ContactUsViewModel
    {
        [Required]
        [MinLength(5, ErrorMessage = "Name is too short")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(250)]
        public string Message { get; set; }
    }
}
