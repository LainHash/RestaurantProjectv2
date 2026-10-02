using MediatR;
using Restaurant.Contract.DTOs.Inventory.IngredientCategories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.IngredientCategories.Commands.Update
{
    public record UpdateIngredientCategoryCommand(Guid Id, UpdateIngredientCategoryRequest Body)
        : IRequest<Result<IngredientCategoryResponse>>
    {
    }
}
