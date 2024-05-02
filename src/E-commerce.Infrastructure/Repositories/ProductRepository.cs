using AutoMapper;
using E_commerce.Core.Dtos;
using E_commerce.Core.Entities;
using E_commerce.Core.Interfaces;
using E_commerce.Infrastructure.Data;
using Microsoft.Extensions.FileProviders;


namespace E_commerce.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext dbContext,
            IFileProvider fileProvider,
            IMapper mapper
            ) : base(dbContext)
        {
            _dbContext = dbContext;
            _fileProvider = fileProvider;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(CreateProductDto productDto)
        {
            if (productDto == null || string.IsNullOrEmpty(productDto.Name))
            {
                throw new ArgumentException("ProductDto is null or invalid.");
            }

            var src = "";
            if (productDto.Image is not null)
            {
                // Upload Image
                var root = "/images/products/";
                var prodName = $"{Guid.NewGuid()}{productDto.Image.FileName}";
                if (!Directory.Exists($"wwwroot{root}"))
                {
                    Directory.CreateDirectory($"wwwroot{root}");
                }
                src = root + prodName;
                var picInfo = _fileProvider.GetFileInfo(src);
                var rootPath = picInfo.PhysicalPath;
                using (var fileStream = new FileStream(rootPath, FileMode.Create))
                {
                    await productDto.Image.CopyToAsync(fileStream);
                }
            }

            // Create New Product
            var mappedProduct = _mapper.Map<Product>(productDto);
            mappedProduct.ProductPicture = src;

            await _dbContext.Products.AddAsync(mappedProduct);
            await _dbContext.SaveChangesAsync();

            return true;

        }

        public async Task<bool> UpdateAsync(int id, UpdateProductDto productDto)
        {
            // Update product with Image
            var currentProduct = await _dbContext.Products.FindAsync(id);
            if (currentProduct is not null)
            {
                _mapper.Map(productDto, currentProduct);

                if (productDto.Image is not null)
                {
                    var root = "/images/products/";
                    var prodName = $"{Guid.NewGuid()}" + productDto.Image.FileName;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{root}", prodName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await productDto.Image.CopyToAsync(fileStream);
                    }

                    // remove old picture
                    if (!string.IsNullOrEmpty(currentProduct.ProductPicture))
                    {
                        var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{currentProduct.ProductPicture}");
                        if (File.Exists(oldFilePath))
                        {
                            File.Delete(oldFilePath);
                        }
                    }

                    // Updating product picture
                    currentProduct.ProductPicture = root + prodName;
                }

                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;

        }

        public async Task<bool> DeleteWithPictureAsync(int id)
        {
            var currentProduct = await _dbContext.Products.FindAsync(id);
            if (currentProduct is not null)
            {
                // remove old picture
                if (!string.IsNullOrEmpty(currentProduct.ProductPicture))
                {
                    // delete old picture
                    var picInfo = _fileProvider.GetFileInfo(currentProduct.ProductPicture);
                    var rootPath = picInfo.PhysicalPath;
                    File.Delete(rootPath);
                }

                _dbContext.Products.Remove(currentProduct);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }



    }
}
