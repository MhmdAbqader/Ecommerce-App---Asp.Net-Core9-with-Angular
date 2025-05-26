using System.Security.Claims;
using Ecommerce.Api.Errors;
using Ecommerce.Core.DTOs;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models;
using Ecommerce.Infrastructure.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _token;
        private readonly IHttpContextAccessor _access;
     

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, ITokenService tokenService, 
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _token = tokenService;
            _access = httpContextAccessor;
       
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto userLoginDto)
        {

            var userExist = await _userManager.FindByEmailAsync(userLoginDto.Email);
            if (userExist == null) { return Unauthorized(new BaseCommonResponseError(401)); }

            var checkPass = await _signInManager.CheckPasswordSignInAsync(userExist, userLoginDto.Password, true);
            if (checkPass is null || !checkPass.Succeeded)
            {
                return Unauthorized(new BaseCommonResponseError(401 , "UserName or Password is Incorrect You have 3 Trail!")); 
            }

            return Ok(new UserDto
            {
                Email = userExist.Email,
                FullName = userExist.FullName,
                Token = _token.CreateToken(userExist)
            });

        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (CheckEmailExist(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new List<string> { "This Email is Already Token" });
            }
            var user = new ApplicationUser
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
                FullName = registerDto.FullName,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded == false) return BadRequest(new BaseCommonResponseError(400));
            
            return Ok(new UserDto { Email = user.Email, FullName = user.FullName, Token = _token.CreateToken(user) });
        }


        [HttpGet("ActiveUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {

            var usr = await _userManager.FindByEmailAsync(HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value);       

            // Retrieve the current user's claims
            var claims = _access.HttpContext.User.Claims;

            // Example: Get the user's email from the claims
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value; // this is working well
                                                                                       //var email = claims.FirstOrDefault(c => c.Type == "email")?.Value;// give me Null value
            var user = await _userManager.FindByEmailAsync(email);

            return new UserDto
            {
                Email = user.Email,
                FullName = user.FullName,
                Token = _token.CreateToken(user)
            };
        }

        [HttpGet("CheckEmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExist([FromQuery] string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            if (result is not null)
            {
                return true;
            }
            return false;
        }

        [HttpGet("UserAddress")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var claims = _access.HttpContext.User.Claims;             
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            // var address = await _userManager.GetUserAddressExtensionMethod(_access); // create Extension Method
                                                                                        // to Include and get address
            // access address by include method of propperty Users

            var userAddress = await _userManager.Users.Include(a => a.Address).SingleOrDefaultAsync(a => a.Email == email);
            var address = new AddressDto 
            {
                FirstName = userAddress?.Address.FirstName,
                LastName = userAddress?.Address.LastName,
                City = userAddress?.Address.City,
                Street = userAddress?.Address.Street,
                ApplicationUserId = userAddress?.Address.ApplicationUserId,
                State = userAddress?.Address.State,
                ZipCode = userAddress?.Address.ZipCode
            };
            return Ok(address);
        }

   

    }
}
