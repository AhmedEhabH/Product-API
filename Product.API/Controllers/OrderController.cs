using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.API.Errors;
using Product.Core.Dto;
using Product.Core.Entities.Orders;
using Product.Core.Interface;
using Product.Core.Services;
using System.Net;
using System.Security.Claims;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IOrderServices _orderServices;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IMapper mapper, IUnitOfWork uow, IOrderServices orderServices, ILogger<OrderController> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _orderServices = orderServices;
            _logger = logger;
        }
        [HttpPost("create-order")]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            _logger.LogInformation($"Order Dto: {orderDto}");
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var address = _mapper.Map<AddressDto, ShipAddress>(orderDto.ShipToAddress);
            _logger.LogInformation($"Mapping <AddressDto, ShipAddress> done : {address}");
            var order = await _orderServices.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);
            _logger.LogInformation($"Order Created.\n{order}");

            _logger.LogInformation($"Order: {order}");

            if (order is null)
            {
                return BadRequest(new BaseCommonResponse(400, "Error, while creating order"));
            }

            return Ok(order);
        }
    }
}
