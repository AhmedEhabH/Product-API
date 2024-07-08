using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Entities;
using Product.Core.Interface;
using Product.Core.Dto;
using Product.Infrastructure.Data;
using Product.Infrastructure.Repository;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(IUnitOfWork uow, IMapper mapper, ILogger<CategoryController> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet("get-all-category")]
        public async Task<ActionResult> Get()
        {
            var allCategory = await _uow.CategoryRepository.GetAllAsync();
            if (allCategory != null)
            {
                var res = _mapper.Map<IReadOnlyList<Category>, IReadOnlyList<ListCategoryDto>>(allCategory);
                return Ok(res);
            }
            return BadRequest("Not Found");
        }

        [HttpGet("get-category-by-id/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var category = await _uow.CategoryRepository.GetAsync(id);
            if (category != null)
            {
                return Ok(_mapper.Map<Category, ListCategoryDto>(category));
            }
            return BadRequest($"Not Found this id {id}");
        }

        [HttpPost("add-new-category")]
        public async Task<ActionResult> Post(CategoryDto category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    /*var newCategory = new Category
                    {
                        Name = category.Name,
                        Description = category.Description,
                    };*/
                    var res = _mapper.Map<Category>(category);
                    await _uow.CategoryRepository.AddAsync(res);
                    return Ok(res);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-existing-category-by-id/{id}")]
        public async Task<ActionResult> Put(int id, CategoryDto category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingCategory = await _uow.CategoryRepository.GetAsync(id);
                    if (existingCategory != null)
                    {
                        _mapper.Map(category, existingCategory);
                        await _uow.CategoryRepository.UpdateAsync(id, existingCategory);
                        return Ok(existingCategory);
                    }
                }
                return BadRequest($"Category id [{id}] Not Found.");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-category-by-id/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            //await _uow.CategoryRepository.DeleteAsync(id);
            //return Ok();
            try
            {
                var existingCategory = await _uow.CategoryRepository.GetAsync(id);
                if (existingCategory != null)
                {
                    await _uow.CategoryRepository.DeleteAsync(id);
                    return Ok($"This Category [{existingCategory.Name}] Is deleted");
                }
                return BadRequest($"Category id [{id}] Not Found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}", ex);
                return BadRequest(ex.Message);
            }
        }

    }
}
