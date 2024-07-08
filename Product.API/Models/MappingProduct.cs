using AutoMapper;
using Product.Core.Entities;
using Product.Core.Dto;
using Product.Infrastructure.Data;

namespace Product.API.Models
{
    public class MappingProduct : Profile
    {
        public MappingProduct()
        {
            CreateMap<Products, ProductDto>()
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name)).ReverseMap();
            CreateMap<Products, CreateProductDto>().ReverseMap();
        }
    }
}
