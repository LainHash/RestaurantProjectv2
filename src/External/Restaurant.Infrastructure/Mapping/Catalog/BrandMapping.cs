using AutoMapper;
using Restaurant.Contract.DTOs.Catalog.Brands;
using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Infrastructure.Mapping.Catalog
{
    internal class BrandMapping : Profile
    {
        public BrandMapping()
        {
            CreateMap<Brand, BrandResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.BrandImages));

            CreateMap<Brand, BrandDetailResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.BrandImages))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            CreateMap<CreateBrandRequest, Brand>();

            CreateMap<UpdateBrandRequest, Brand>();
        }
    }
}
