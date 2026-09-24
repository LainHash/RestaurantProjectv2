using AutoMapper;
using Restaurant.Contract.DTOs.Commerce.WishlistItems;
using Restaurant.Domain.Entities.Commerce;

namespace Restaurant.Infrastructure.Mapping.Commerce
{
    internal class WishlistItemMapping : Profile
    {
        public WishlistItemMapping()
        {
            CreateMap<WishlistItem, WishlistItemResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.PublicId))
                .ForMember(dest => dest.PrimaryImage, opt => opt.MapFrom(src => src.Product.ProductImages.First(x => x.IsPrimary).Image.Url))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product.ProductPrice.UnitPrice))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Product.ProductPrice.Currency));
        }
    }
}
