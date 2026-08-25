using AutoMapper;
using Restaurant.Contract.DTOs.Storage.Images;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Infrastructure.Mapping.Storage
{
    internal class ImageMapping : Profile
    {
        public ImageMapping()
        {
            CreateMap<Image, ImageResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.IsPrimary, opt => opt.MapFrom(src => src.ProductImage.IsPrimary))
                .ForMember(dest => dest.DisplayOrder, opt => opt.MapFrom(src => src.ProductImage.DisplayOrder));

            CreateMap<CloudinaryUploadResult, Image>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PublicId, opt => opt.Ignore());

            CreateMap<Image, UploadImageResponse>();
        }
    }
}
