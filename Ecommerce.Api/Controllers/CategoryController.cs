 
using Ecommerce.Core.DTOs;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> Get() 
        {
            var catList = await _unitOfWork.CategoryRepository.GetAllAsync();
            if(catList is not null)
                return Ok(catList.Select(a=>new ListCategoryDto { Id = a.Id, Name = a.Name , Description = a.Description}));
            return BadRequest("Not any category founded");
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(i => i.Id == id,null);
            if (category is not null)
            {
                var res = new CategoryDto {  Name = category.Name, Description = category.Description };
                return Ok(res);
            }

            return BadRequest($"Not any category founded with this id : {id}");
        }


        [HttpPost("Create-Category")]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category { Name = categoryDto.Name, Description = categoryDto.Description };
                await _unitOfWork.CategoryRepository.AddAsync(category);
                return Ok(category);
            }
            else
                return BadRequest(categoryDto);
        }



        [HttpPut("Update-Category")]
        public async Task<IActionResult> Update(int id, CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                Category existCategory = await _unitOfWork.CategoryRepository.GetByIdAsync(i => i.Id == id, null);
                if (existCategory is not null)
                {
                    existCategory.Name = categoryDto.Name;
                    existCategory.Description = categoryDto.Description;
                   // await _unitOfWork.CategoryRepository.UpdateAsync(id,existCategory);
                    await _unitOfWork.CategoryRepository.UpdateAsync(existCategory);
                    return Ok(existCategory);
                }
                return BadRequest($"No any category founded with this id : {id}");
            }
            else
                return BadRequest(categoryDto);
        }

        [HttpDelete("Delete-Category")]
        public async Task<IActionResult> Delete(int id)
        {

            Category existCategory = await _unitOfWork.CategoryRepository.GetByIdAsync(i => i.Id == id, null);
            if (existCategory is not null)
            {
                await _unitOfWork.CategoryRepository.DeleteAsync(id);
                return Ok("Deleted Successfully!");
            }
            return BadRequest($"No any category founded with this id : {id}");
            
        }
    }
}
