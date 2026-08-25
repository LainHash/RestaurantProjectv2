using AutoMapper;
using Restaurant.Contract.DTOs.Storage.Images;
using Restaurant.Domain.Entities.Storage;

namespace Restaurant.Infrastructure.Mapping.Storage
{
    internal class ProductImageMapping : Profile
    {
        public ProductImageMapping()
        {
            CreateMap<ProductImage, ImageResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Image.PublicId))
                .ForMember(dest => dest.AltText, opt => opt.MapFrom(src => src.Image.AltText))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Image.Url));

            CreateMap<ProductImage, UploadImageResponse>();
        }
    }
}
