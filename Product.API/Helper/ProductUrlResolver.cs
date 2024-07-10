using AutoMapper;
using Product.Core.Dto;
using Product.Core.Entities;

namespace Product.API.Helper
{
    public class ProductUrlResolver : IValueResolver<Products, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Products source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.ProductPicture)) 
            {
                return _configuration["API.URL"] + source.ProductPicture;
            }
            return null;
        }
    }
}
