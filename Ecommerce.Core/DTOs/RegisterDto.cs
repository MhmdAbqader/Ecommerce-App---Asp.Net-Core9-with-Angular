﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTOs
{
    public class RegisterDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "Min Length is 2 character")]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$", ErrorMessage = "Password It expects at least 1 small-case letter, 1 Capital letter, 1 digit, 1 special character and the length should be between 6-10 characters")]
        public string Password { get; set; }
    }
}
