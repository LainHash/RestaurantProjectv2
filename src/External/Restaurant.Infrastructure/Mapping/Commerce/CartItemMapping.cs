using AutoMapper;
using Restaurant.Contract.DTOs.Commerce.CartItems;
using Restaurant.Domain.Entities.Commerce;

namespace Restaurant.Infrastructure.Mapping.Commerce
{
    internal class CartItemMapping : Profile
    {
        public CartItemMapping()
        {
            CreateMap<CartItem, CartItemResponse>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product.ProductPrice.UnitPrice))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Product.ProductPrice.Currency))
                .ForMember(dest => dest.LineTotal, opt => opt.MapFrom(src => src.Product.ProductPrice.UnitPrice * src.Quantity));
        }
    }
}
