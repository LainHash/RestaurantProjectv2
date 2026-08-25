using AutoMapper;
using Restaurant.Contract.DTOs.Production.Recipes;
using Restaurant.Domain.Entities.Production;

namespace Restaurant.Infrastructure.Mapping.Production
{
    internal class RecipeMapping : Profile
    {
        public RecipeMapping()
        {
            CreateMap<Recipe, RecipeResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.RecipeIngredients, opt => opt.MapFrom(src => src.RecipeIngredients));

            CreateMap<CreateRecipeRequest, Recipe>();
            CreateMap<UpdateRecipeRequest, Recipe>();
        }
    }
}
