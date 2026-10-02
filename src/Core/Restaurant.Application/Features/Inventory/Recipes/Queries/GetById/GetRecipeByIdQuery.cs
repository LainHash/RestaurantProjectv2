using MediatR;
using Restaurant.Contract.DTOs.Inventory.Recipes;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.Recipes.Queries.GetById
{
    public record GetRecipeByIdQuery(Guid Id)
        : IRequest<Result<RecipeResponse>>
    {
    }
}
