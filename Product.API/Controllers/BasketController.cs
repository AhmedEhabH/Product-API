using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Entities;
using Product.Core.Interface;
using Product.Infrastructure.Repository;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public BasketController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet("get-basket-item/{Id}")]
        public async Task<IActionResult> GetBasketById(string Id)
        {
            var _basket = await _uow.BasketRepository.GetBasketAsync(Id);
            return Ok(_basket ?? new CustomerBasket(Id));
        }

        [HttpPost("update-basket")]
        public async Task<IActionResult> UpdateBasket(CustomerBasket customerBasket)
        {
            var _basket = await _uow.BasketRepository.UpdateBasketAsync(customerBasket);
            return Ok(_basket);
        }

        [HttpDelete("delete-basket-item/{Id}")]
        public async Task<IActionResult> DeleteBasket(string Id)
        {
            return Ok(await _uow.BasketRepository.DeleteBasketAsync(Id));
        }
    }
}
