using E_commerce.Core.Sharing;
using E_commerce.Core.Dtos;
using E_commerce.Core.Entities;

namespace E_commerce.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        // For Future
        Task<bool> AddAsync(CreateProductDto productDto);
        Task<bool> UpdateAsync(int id, UpdateProductDto productDto);
        Task<bool> DeleteWithPictureAsync(int id);
        Task<ReturnProductDto> GetAllAsync(ProductParams productParams);

    }
}
