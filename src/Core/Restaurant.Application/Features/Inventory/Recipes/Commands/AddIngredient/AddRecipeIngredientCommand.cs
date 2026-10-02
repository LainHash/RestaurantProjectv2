using MediatR;
using Restaurant.Contract.DTOs.Inventory.RecipeIngredients;
using Restaurant.Contract.DTOs.Inventory.Recipes;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.Recipes.Commands.AddIngredient
{
    public record AddRecipeIngredientCommand(Guid Id, IEnumerable<AddRecipeIngredientRequest> Body)
        : IRequest<Result<RecipeResponse>>
    {
    }
}
