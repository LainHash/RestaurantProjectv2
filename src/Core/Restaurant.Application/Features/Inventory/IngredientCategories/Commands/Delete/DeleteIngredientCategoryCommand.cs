using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.IngredientCategories.Commands.Delete
{
    public record DeleteIngredientCategoryCommand(Guid Id)
        : IRequest<Result>
    {
    }
}
