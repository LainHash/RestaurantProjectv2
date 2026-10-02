using MediatR;
using Restaurant.Contract.DTOs.Inventory.IngredientCategories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.IngredientCategories.Commands.Create
{
    public record CreateIngredientCategoryCommand(CreateIngredientCategoryRequest Body)
        : IRequest<Result<IngredientCategoryResponse>>
    {
    }
}
