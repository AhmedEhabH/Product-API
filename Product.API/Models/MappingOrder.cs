using AutoMapper;
using Product.Core.Dto;
using Product.Core.Entities.Orders;

namespace Product.API.Models
{
    public class MappingOrder : Profile
    {
        public MappingOrder()
        {
            CreateMap<AddressDto, ShipAddress>().ReverseMap();
        }
    }
}
