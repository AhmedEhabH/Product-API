using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Infrastructure.Data;
using Product.Core.Interface;
using Product.Core.Entities;
using Product.Core.Dto;
using Product.API.Errors;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ProductController(IUnitOfWork uow, IMapper mapper, ILogger<ProductController> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet("get-all-products")]
        public async Task<ActionResult> Get()
        {
            var products = await _uow.ProductRepository.GetAllAsync(x => x.Category);
            var result = _mapper.Map<List<ProductDto>>(products);
            return Ok(result);
        }

        [HttpGet("get-product-by-id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseCommonResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById(int id)
        {
            var product = await _uow.ProductRepository.GetByIdAsync(id, x => x.Category);
            if (product is null) return NotFound(new BaseCommonResponse(404));
            var result = _mapper.Map<ProductDto>(product);
            return Ok(result);
        }

        [HttpPost("add-new-product")]
        public async Task<ActionResult> Post([FromForm] CreateProductDto product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _uow.ProductRepository.AddAsync(product);
                    return result ? Ok(product) : BadRequest(result);
                }
                return BadRequest(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-existing-product/{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] UpdateProductDro dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _uow.ProductRepository.UpdateAsync(id, dto);
                    return result ? Ok(dto) : BadRequest(result);
                }
                return BadRequest($"Model State Not Valid, {dto}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-existing-product/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _uow.ProductRepository.DeleteAsync(id);
                    return result ? Ok(result) : BadRequest(result);
                }
                return NotFound($"This id={id} not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
