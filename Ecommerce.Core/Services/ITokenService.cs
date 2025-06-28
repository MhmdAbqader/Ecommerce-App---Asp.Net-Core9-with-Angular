using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Core.Models;

namespace Ecommerce.Core.Services
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}
