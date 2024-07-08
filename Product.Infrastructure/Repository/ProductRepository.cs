using Microsoft.Extensions.FileProviders;
using Product.Infrastructure.Data;
using AutoMapper;
using Product.Core.Entities;
using Product.Core.Interface;
using Product.Core.Dto;

namespace Product.Infrastructure.Repository
{
    public class ProductRepository : GenericRepository<Products>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext context, IFileProvider fileProvider, IMapper mapper) : base(context)
        {
            _context = context;
            _fileProvider = fileProvider;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(CreateProductDto productDto)
        { 
            if(productDto.Image is not null)
            {
                var root = "/image/product";
                var productName = $"{Guid.NewGuid()}.{productDto.Image.FileName}";
                if(!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                var src = root + productName;
                var picInfo = _fileProvider.GetFileInfo(src);
                var rootPath = picInfo.PhysicalPath;
                using(var fileStream = new FileStream(rootPath, FileMode.Create))
                {
                    await productDto.Image.CopyToAsync(fileStream);
                }
                var result = _mapper.Map<Products>(productDto);
                result.ProductPicture = src;
                await _context.Products.AddAsync(result);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
