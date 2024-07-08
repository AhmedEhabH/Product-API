using AutoMapper;
using Product.Core.Entities;
using Product.Core.Dto;
using Product.Infrastructure.Data;
namespace Product.API.Models
{
    public class MappingCategory : Profile
    {
        public MappingCategory()
        {
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<ListCategoryDto, Category>().ReverseMap();
        }
    }
}
