using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BasketController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetBasketItem/{id}")]
        public async Task<IActionResult> GetBasketItem(string id) 
        {
            var basket = await _unitOfWork.BasketRepository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost("UpdateBasketItem")]
        public async Task<IActionResult> UpdateBasketItem(CustomerBasket customerBasket)
        {
            var basket = await _unitOfWork.BasketRepository.UpdateBasketAsync(customerBasket);
            return Ok(basket);
        }


        [HttpGet("DeleteBasketItem/{id}")]
        public async Task<IActionResult> DeleteBasketItem(string id)
        {
            var basket = await _unitOfWork.BasketRepository.DeleteBasketAsync(id);
            return Ok(basket);
        }
    }
}
