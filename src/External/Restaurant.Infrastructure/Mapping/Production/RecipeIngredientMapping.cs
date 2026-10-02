using AutoMapper;
using Restaurant.Contract.DTOs.Inventory.RecipeIngredients;
using Restaurant.Domain.Entities.Inventory;

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
