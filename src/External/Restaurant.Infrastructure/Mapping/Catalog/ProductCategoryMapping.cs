using AutoMapper;
using Restaurant.Contract.DTOs.Catalog.Categories;
using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Infrastructure.Mapping.Catalog
{
    internal class ProductCategoryMapping : Profile
    {
        public ProductCategoryMapping()
        {
            CreateMap<ProductCategory, ProductCategoryResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId));

            CreateMap<CreateProductCategoryRequest, ProductCategory>();

            CreateMap<UpdateProductCategoryRequest, ProductCategory>();
        }
    }
}
