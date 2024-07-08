using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Infrastructure.Data;
using Product.Core.Interface;
using Product.Core.Entities;
using Product.Core.Dto;

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
        public async Task<ActionResult> GetById(int id)
        {
            var product = await _uow.ProductRepository.GetByIdAsync(id, x => x.Category);
            var result = _mapper.Map<ProductDto>(product);
            return Ok(result);
        }

        [HttpPost("add-new-product")]
        public async Task<ActionResult> Post(CreateProductDto product)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var result = _mapper.Map<Products>(product);
                    await _uow.ProductRepository.AddAsync(result);
                    return Ok(result);
                }
                return BadRequest(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
