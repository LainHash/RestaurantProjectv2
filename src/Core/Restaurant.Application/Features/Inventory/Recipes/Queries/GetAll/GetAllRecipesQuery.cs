using MediatR;
using Restaurant.Contract.DTOs.Inventory.Recipes;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.Recipes.Queries.GetAll
{
    public record GetAllRecipesQuery()
        : IRequest<Result<IEnumerable<RecipeResponse>>>
    {
    }
}
