using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Infrastructure.Data
{
    public class SeedingDefaultUser
    {
        //private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly UserManager<ApplicationUser> _userManager;

        //public SeedingDefaultUser(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        //{
        //    _roleManager = roleManager;
        //    _userManager = userManager;
        //}
        public static async Task SeedingUserAsync(UserManager<ApplicationUser> _userManager) 
        {
            if (!_userManager.Users.Any()) 
            {
                var user = new ApplicationUser
                {
                    FullName = "Mhmd Abqader",
                    Email = "mhmd@api.com",
                    UserName = "mhmd@api.com",
                    Address = new Address() { FirstName = "mhmd", LastName = "abqader", Street = "st7", City = "nasrcity", State = "bla", ZipCode = "12Qzv" }
                };

                var result = await _userManager.CreateAsync(user,"Asdasd!1");
            }
        }

    }
}
