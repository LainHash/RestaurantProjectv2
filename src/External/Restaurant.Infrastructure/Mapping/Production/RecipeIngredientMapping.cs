using AutoMapper;
using Restaurant.Contract.DTOs.Production.RecipeIngredients;
using Restaurant.Domain.Entities.Production;

namespace Restaurant.Infrastructure.Mapping.Production
{
    internal class RecipeIngredientMapping : Profile
    {
        public RecipeIngredientMapping()
        {
            CreateMap<RecipeIngredient, RecipeIngredientResponse>()
                .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Ingredient.Name))
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.Symbol));

            CreateMap<AddRecipeIngredientRequest, RecipeIngredient>();
        }
    }
}
