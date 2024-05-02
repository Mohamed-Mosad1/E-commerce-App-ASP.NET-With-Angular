using AutoMapper;
using E_commerce.Core.Dtos;
using E_commerce.Core.Entities;

namespace E_commerce.API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.ProductPicture)) 
            {
                return _configuration["BaseApiUrl"] + source.ProductPicture;
            }

            return null;
        }
    }
}
