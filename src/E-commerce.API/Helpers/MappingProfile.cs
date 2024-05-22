using AutoMapper;
using E_commerce.API.Helpers;
using E_commerce.Core.Dtos;
using E_commerce.Core.Entities;
using E_commerce.Core.Entities.Identity;

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


        }
    }
}
