using Product.Core.Entities;
using Product.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Interface
{
    public interface IProductRepository : IGenericRepository<Products>
    {
        Task<bool> AddAsync(CreateProductDto dto);
        Task<bool> UpdateAsync(int id, UpdateProductDro dto);
        Task<bool> DeleteAsync(int id);
    }
}
