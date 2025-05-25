using Ecommerce.Api.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    //[Route("api/[controller]")]
    [Route("errors/{statusCode}")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Error(int statusCode) 
        {
            return new ObjectResult(new BaseCommonResponseError(statusCode));
        }
    }
}
