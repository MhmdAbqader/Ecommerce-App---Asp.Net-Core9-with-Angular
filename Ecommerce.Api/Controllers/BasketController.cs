using Ecommerce.Core.DTOs;
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
        public async Task<IActionResult> UpdateBasketItem(CustomerBasketDto customerBasketDto)
        {
            CustomerBasket customerBasket = new CustomerBasket() 
            {
                UserId = customerBasketDto.userId,                
            };
            foreach (var item in customerBasketDto.BasketItemsDto)
            {
                customerBasket.BasketItems.Add(new BasketItem 
                { 
                    Id = item.Id, 
                    ProductName = item.ProductName,
                    CategoryName = item.categoryName,
                    Price = item.Price,
                    Quantity = item.Quantity
                });
            }

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
