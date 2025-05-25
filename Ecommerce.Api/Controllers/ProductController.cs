 
using Ecommerce.Core.DTOs;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> Get([FromQuery] string? search, [FromQuery]string? mySort, [FromQuery] int? catId, int pageNo, int pageSize)
        {
           // var productList = await _unitOfWork.ProductRepository.GetAllAsync();
            var productList = await _unitOfWork.ProductRepository.GetAllAsync(search, mySort, catId,pageNo,pageSize);
            if (productList is not null)
                return Ok(productList.Select(a =>
                new {
                    Id = a.Id, Name = a.Name, Description = a.Description, Price =a.Price,
                    categoryName = a.CategoryName,
                    imgURL=a.Img, totalCount = a.Totalcount, pageNo = a.PageNo, pageSize = a.PageSize
                }));
            return BadRequest("Not any product founded");
        }


        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(i => i.Id == id, "Category");
            if ( product is not null)
            {
                var res = new ProductDto 
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Img = product.ImgURL,
                    CategoryName= product.Category.Name 
                };
                return Ok(res);
            }

            return BadRequest($"Not any category founded with this id : {id}");
        }

        [HttpPost("Create-Product")]
        public async Task<IActionResult> Create(CreateProductDto  createProductDto )
        {
            if (ModelState.IsValid)
            {
             
              bool result = await _unitOfWork.ProductRepository.AddAsync(createProductDto);
              return result? Ok(createProductDto) : BadRequest();
            }
            else
                return BadRequest(createProductDto);
        }

        [HttpDelete("Delete-Product/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id > 0)
            {
                bool result = await _unitOfWork.ProductRepository.DeleteWithImageAsync(id);
                return result ? Ok(result) : BadRequest();
            }
            else
                return NotFound($"No Product with this id = {id}");
        }
    }
}
