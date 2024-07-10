using AutoMapper;
using Product.Core.Entities;
using Product.Core.Dto;
using Product.API.Helper;

namespace Product.API.Models
{
    public class MappingProduct : Profile
    {
        public MappingProduct()
        {
            CreateMap<Products, ProductDto>()
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.ProductPicture, o => o.MapFrom<ProductUrlResolver>())
                .ReverseMap();

            CreateMap<Products, CreateProductDto>().ReverseMap();
            CreateMap<Products, UpdateProductDro>().ReverseMap();
        }
    }
}
