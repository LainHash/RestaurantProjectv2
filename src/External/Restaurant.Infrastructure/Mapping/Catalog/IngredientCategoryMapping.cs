using AutoMapper;
using Restaurant.Contract.DTOs.Catalog.IngredientCategories;
using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Infrastructure.Mapping.Catalog
{
    internal class IngredientCategoryMapping : Profile
    {
        public IngredientCategoryMapping()
        {
            CreateMap<IngredientCategory, IngredientCategoryResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId));

            CreateMap<CreateIngredientCategoryRequest, IngredientCategory>();

            CreateMap<UpdateIngredientCategoryRequest, IngredientCategory>();
        }
    }
}
