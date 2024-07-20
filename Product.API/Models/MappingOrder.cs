using AutoMapper;
using Product.API.Helper;
using Product.Core.Dto;
using Product.Core.Entities.Orders;

namespace Product.API.Models
{
    public class MappingOrder : Profile
    {
        public MappingOrder()
        {
            CreateMap<ShipAddress, AddressDto>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.OrderId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price))
                .ReverseMap();
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductItemId, o => o.MapFrom(s => s.OrderItemID))
                .ForMember(d => d.ProductItemName, o => o.MapFrom(s => s.ProductItemOrder.ProductItemName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ProductItemOrder.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>())
                .ReverseMap();
        }
    }
}
