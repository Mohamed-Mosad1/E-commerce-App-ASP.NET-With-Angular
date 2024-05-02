using AutoMapper;
using E_commerce.Core.Dtos;
using E_commerce.Core.Entities;
using E_commerce.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCategories()
        {
            var allCategories = await _unitOfWork.CategoryRepository.GetAllAsync();

            if (allCategories is not null && allCategories.Any())
            {
                var res = _mapper.Map<IReadOnlyList<Category>, IReadOnlyList<ListingCategoryDto>>(allCategories);

                return Ok(res);
            }

            return BadRequest("Not Found");
        }


        [HttpGet("categoryById{id}")]
        public async Task<ActionResult> GetCategoryById(int id)
        {
            var categories = await _unitOfWork.CategoryRepository.GetAsync(id);
            if (categories is null)
                return BadRequest($"Not Found This id: [{id}]");

            var res = _mapper.Map<Category, ListingCategoryDto>(categories);

            return Ok(res);
        }

        [HttpPost("addNewCategory")]
        public async Task<ActionResult> AddCategory(CategoryDto categoryDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = _mapper.Map<Category>(categoryDto);

                    await _unitOfWork.CategoryRepository.AddAsync(res);
                    return Ok(categoryDto);
                }
                return BadRequest(categoryDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateCategory")]
        public async Task<ActionResult> UpdateCategory(UpdateCategoryDto categoryDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var currentCategory = await _unitOfWork.CategoryRepository.GetAsync(categoryDto.Id);

                    if (currentCategory is not null)
                    {
                        // Updating
                        _mapper.Map(categoryDto, currentCategory);
                        await _unitOfWork.CategoryRepository.UpdateAsync(categoryDto.Id, currentCategory);
                        return Ok(categoryDto);
                    }
                    else
                    {
                        return NotFound($"Category Not Found, Id: [{categoryDto.Id}] is wrong");

                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }

            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("deleteCategoryById{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var currentCategory = await _unitOfWork.CategoryRepository.GetAsync(id);

                    if (currentCategory is not null)
                    {
                        await _unitOfWork.CategoryRepository.DeleteAsync(id);
                        return Ok($"This Category [{currentCategory.Name}] has been deleted Successfully");
                    }
                }
                return BadRequest($"Category Not Found, Id: [{id}] is wrong");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





    }
}
