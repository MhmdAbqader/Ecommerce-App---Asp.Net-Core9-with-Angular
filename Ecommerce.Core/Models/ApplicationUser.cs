using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
            public string FullName { get; set; }
            public virtual Address Address { get; set; }
     
    }
}
