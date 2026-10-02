using MediatR;
using Restaurant.Contract.DTOs.Inventory.Recipes;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.Recipes.Commands.Update
{
    public record UpdateRecipeCommand(Guid Id, UpdateRecipeRequest Body)
        : IRequest<Result<RecipeResponse>>
    {
    }
}
