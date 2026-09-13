using AutoMapper;
using Restaurant.Contract.DTOs.Catalog.Products;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Enums;

namespace Restaurant.Infrastructure.Mapping.Catalog
{
    internal class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.ProductPrice.UnitPrice))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.ProductPrice.Currency))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand!.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.ProductCategory.Name))
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.Symbol))
                .ForMember(dest => dest.PrimaryImage, opt => opt.MapFrom(src => src.ProductImages
                                                                    .First(x => x.ProductId == src.Id && x.IsPrimary)));

            CreateMap<Product, ProductDetailResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.ProductPrice.UnitPrice))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.ProductPrice.Currency))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand!.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.ProductCategory.Name))
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.Symbol))
                .ForMember(dest => dest.PrimaryImage, opt => opt.MapFrom(src => src.ProductImages
                                                                    .First(x => x.ProductId == src.Id && x.IsPrimary)))
                .ForMember(dest => dest.Recipes, opt => opt.MapFrom(src => src.Recipes))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.ProductImages));

            CreateMap<Product, ProductMinimalResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId));

            CreateMap<CreateProductRequest, Product>()
                .ForPath(dest => dest.ProductPrice.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForPath(dest => dest.ProductPrice.Currency, opt => opt.MapFrom(src => src.Currency));

            CreateMap<UpdateProductRequest, Product>()
                .ForPath(dest => dest.ProductPrice.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForPath(dest => dest.ProductPrice.Currency, opt => opt.MapFrom(src => src.Currency));
        }
    }
}
