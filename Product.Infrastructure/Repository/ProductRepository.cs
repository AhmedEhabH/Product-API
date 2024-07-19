using Microsoft.Extensions.FileProviders;
using Product.Infrastructure.Data;
using AutoMapper;
using Product.Core.Entities;
using Product.Core.Interface;
using Product.Core.Dto;
using Product.Core.Sharing;
using Microsoft.EntityFrameworkCore;

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
            if (productDto.Image is not null)
            {
                var root = "/images/product/";
                var productName = $"{Guid.NewGuid()}.{productDto.Image.FileName}";
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                var src = root + productName;
                var picInfo = _fileProvider.GetFileInfo(src);
                var rootPath = picInfo.PhysicalPath;
                using (var fileStream = new FileStream(rootPath, FileMode.Create))
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

        public async Task<bool> UpdateAsync(int id, UpdateProductDro dto)
        {
            var currentProduct = await _context.Products.FindAsync(id);
            if (currentProduct is not null)
            {
                var src = "";
                if (dto.Image is not null)
                {
                    var root = "/images/product/";
                    var productName = $"{Guid.NewGuid()}.{dto.Image.FileName}";
                    if (!Directory.Exists(root))
                    {
                        Directory.CreateDirectory(root);
                    }
                    src = root + productName;
                    var picInfo = _fileProvider.GetFileInfo(src);
                    var rootPath = picInfo.PhysicalPath;
                    using (var fileStream = new FileStream(rootPath, FileMode.Create))
                    {
                        await dto.Image.CopyToAsync(fileStream);
                    }
                    if (!string.IsNullOrEmpty(currentProduct.ProductPicture))
                    {
                        picInfo = _fileProvider.GetFileInfo(currentProduct.ProductPicture);
                        rootPath = picInfo?.PhysicalPath;
                        System.IO.File.Delete(rootPath);
                    }
                    var result = _mapper.Map<Products>(dto);
                    result.ProductPicture = src;
                    _context.Products.Update(result);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var currentProduct = await _context.Products.FindAsync(id);
            if (currentProduct is not null)
            {
                if (!string.IsNullOrEmpty(currentProduct.ProductPicture))
                {
                    var picInfo = _fileProvider.GetFileInfo(currentProduct.ProductPicture);
                    var rootPath = picInfo?.PhysicalPath;
                    File.Delete(rootPath);
                }
                _context.Products.Remove(currentProduct);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync(ProductParams productParams)
        {
            var query = await _context.Products.Include(x => x.Category).AsNoTracking().ToListAsync();

            // Search
            if (!string.IsNullOrEmpty(productParams.Search))
            {
                query = query.Where(x => x.Name.ToLower().Contains(productParams.Search)).ToList();
            }
            
            // Filter by category Id
            if (productParams.CategoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == productParams.CategoryId.Value).ToList();
            }

            // Sorting
            if (!string.IsNullOrEmpty(productParams.Sorting))
            {
                query = productParams.Sorting switch
                {
                    "PriceAsc" => query.OrderBy(x => x.Price).ToList(),
                    "PriceDesc" => query.OrderByDescending(x => x.Price).ToList(),
                    _ => query.OrderBy(x => x.Name).ToList(),
                };
            }

            // Pagination
            query = query.Skip(productParams.PageSize * (productParams.PageNumber - 1)).Take(productParams.PageSize).ToList();

            var result = _mapper.Map<List<ProductDto>>(query);
            return result;
        }
    }
}
