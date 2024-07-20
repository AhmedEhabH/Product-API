using AutoMapper;
using AutoMapper.Execution;
using Product.Core.Dto;
using Product.Core.Entities.Orders;

namespace Product.API.Helper
{
    public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _config;

        public OrderItemUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductItemOrder.PictureUrl))
            {
                return _config["API.URL"] + source.ProductItemOrder.PictureUrl;
            }
            return null;
        }
    }
}
