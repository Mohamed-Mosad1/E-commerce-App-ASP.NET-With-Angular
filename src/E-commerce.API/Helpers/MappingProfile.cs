using AutoMapper;
using E_commerce.API.Helpers;
using E_commerce.Core.Dtos;
using E_commerce.Core.Entities;
using E_commerce.Core.Entities.Basket;
using E_commerce.Core.Entities.Identity;
using E_commerce.Core.Entities.OrderAggregate;

namespace E_commerce.API.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<ListingCategoryDto, Category>().ReverseMap();
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();

            CreateMap<Product, ProductDto>()
                .ForMember(D => D.CategoryName, options => options.MapFrom(S => S.Category.Name))
                .ForMember(D => D.ProductPicture, options => options.MapFrom<ProductUrlResolver>())
                .ReverseMap();

            CreateMap<CreateProductDto, Product>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();

            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            
            CreateMap<ShippingAddress, AddressDto>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d=>d.DeliveryMethods, opt=>opt.MapFrom(s=>s.DeliveryMethods.ShortName))
                .ForMember(d=>d.ShippingPrice, opt=>opt.MapFrom(s=>s.DeliveryMethods.Cost))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(p=>p.ProductItemId, opt=>opt.MapFrom(s=>s.Id))
                .ForMember(p=>p.ProductItemName, opt=>opt.MapFrom(s=>s.Product.ProductItemName))
                .ForMember(p=>p.PictureUrl, opt=>opt.MapFrom(s=>s.Product.PictureUrl))
                .ForMember(p=>p.PictureUrl, opt=>opt.MapFrom<OrderItemUrlResolver>())
                .ReverseMap();


        }
    }
}
