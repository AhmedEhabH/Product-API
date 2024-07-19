using AutoMapper;
using Product.Core.Dto;
using Product.Core.Entities;

namespace Product.API.Models
{
    public class MappingUser : Profile
    {
        public MappingUser()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
