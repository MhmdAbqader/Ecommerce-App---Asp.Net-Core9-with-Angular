﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTOs
{
    public class UserDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }

        public string Token { get; set; }
    }
}
