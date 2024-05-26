using AutoMapper;
using E_commerce.Core.Dtos;
using E_commerce.Core.Entities.OrderAggregate;

namespace E_commerce.API.Helpers
{
    public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.PictureUrl))
            {
                return $"{_configuration["BaseApiUrl"]}/{source.Product.PictureUrl}";
            }

            return string.Empty;
        }
    }
}
