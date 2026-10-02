using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.Ingredients.Commands.Restore
{
    public record RestoreIngredientCommand(Guid Id)
        : IRequest<Result>
    {
    }
}
