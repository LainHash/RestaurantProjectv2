using MediatR;
using Restaurant.Contract.DTOs.Inventory.Recipes;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.Recipes.Commands.Create
{
    public record CreateRecipeCommand(CreateRecipeRequest Body)
        : IRequest<Result<RecipeResponse>>
    {
    }
}
