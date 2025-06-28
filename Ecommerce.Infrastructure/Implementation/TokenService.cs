using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Core.Models;
using Ecommerce.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Infrastructure.Implementation
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["token:key"]));
        }

        public string CreateToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email , user.Email),
                new Claim(ClaimTypes.GivenName , user.FullName),
            };

            var credentiasl = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _configuration["token:issuer"],
                SigningCredentials = credentiasl,
                Expires = DateTime.Now.AddSeconds(50)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var myToken = tokenHandler.CreateToken(tokenDescriptor);


            //another way to create token
            //var test = new JwtSecurityToken(

            //    issuer: _configuration["token:issuer"],
            //    claims: claims,
            //    signingCredentials: credentiasl,
            //    expires: DateTime.Now.AddMinutes(5)
            //);
            //var temp = tokenHandler.WriteToken(test);
            //return myToken.ToString();

            return tokenHandler.WriteToken(myToken);
        }
    }
}
