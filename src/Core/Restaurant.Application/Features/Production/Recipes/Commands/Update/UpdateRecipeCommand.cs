using MediatR;
using Restaurant.Contract.DTOs.Production.Recipes;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Production.Recipes.Commands.Update
{
    public record UpdateRecipeCommand(Guid Id, UpdateRecipeRequest Body)
        : IRequest<Result<RecipeResponse>>
    {
    }
}
