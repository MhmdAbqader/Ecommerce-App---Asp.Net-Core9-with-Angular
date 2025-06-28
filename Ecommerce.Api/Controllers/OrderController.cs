using System.Security.Claims;
using Ecommerce.Api.Errors;
using Ecommerce.Core.DTOs;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models.OrderOperations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        //##############  I will Add OrderDto and IMapper Package But Later not  right now ########################

        
        [HttpPost("CreateOrder")]
        [Authorize]
        public async Task<IActionResult> CreateOrder(OrderCreateDto orderDto)
        {
            var emailCustomer = HttpContext.User?.Claims?.FirstOrDefault(a => a.Type == ClaimTypes.Email)?.Value;
                   
            ShipToAddress shipToAddress = new ShipToAddress()
            {
                FirstName = orderDto.ShipToAddressDto.FirstName,
                LastName = orderDto.ShipToAddressDto.LastName,
                City = orderDto.ShipToAddressDto.City,
                State = orderDto.ShipToAddressDto.State,
                Street = orderDto.ShipToAddressDto.Street,
            };

            var orderObject = await _unitOfWork.OrderRepository.CreateOrderAsync(emailCustomer, orderDto.DeliveryMethodId, orderDto.BasketId, shipToAddress);

            if (orderObject is null) return BadRequest(new BaseCommonResponseError(400,"Error Occured!"));

            return Ok(orderObject);
        }


        [HttpGet("GetOrderById/{id}")]
        [Authorize]
        public async Task<IActionResult> GetOrderById(int id)
        {
         
            var orderbyID =await _unitOfWork.OrderRepository.GetByIdAsync(a => a.Id == id, "OrderLines,DeliveryMethod");
            if (orderbyID is null) return BadRequest(new BaseCommonResponseError(400, "Order Not Found in Db!"));

            return Ok(orderbyID);
        }


        [HttpGet("GetOrdersForUserAsync")]
        [Authorize]
        public async Task<IActionResult> GetOrdersForUserAsync()
        {
            var emailCustomer = HttpContext.User?.Claims?.FirstOrDefault(a => a.Type == ClaimTypes.Email)?.Value;
         
            var orderbyemailCustomer = await _unitOfWork.OrderRepository.GetOrdersForUserAsync(emailCustomer);
            if (orderbyemailCustomer is null) return BadRequest(new BaseCommonResponseError(400, "User has No Orders in Db!"));

            return Ok(orderbyemailCustomer);
        }
        [HttpGet("GetAllDeliveryMethods")]
        [Authorize]
        public async Task<IActionResult> GetAllDeliveryMethods()
        {

            var dMethods = await _unitOfWork.OrderRepository.GetDeliveryMethodsAsync();
            if (dMethods is null) return BadRequest(new BaseCommonResponseError(400, "Error Occured while getting Delivery Methods  !"));

            return Ok(dMethods);
        }
    }
}
