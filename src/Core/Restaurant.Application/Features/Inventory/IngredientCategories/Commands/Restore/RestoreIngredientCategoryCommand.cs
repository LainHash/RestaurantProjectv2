using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.IngredientCategories.Commands.Restore
{
    public record RestoreIngredientCategoryCommand(Guid Id)
        : IRequest<Result>
    {
    }
}
