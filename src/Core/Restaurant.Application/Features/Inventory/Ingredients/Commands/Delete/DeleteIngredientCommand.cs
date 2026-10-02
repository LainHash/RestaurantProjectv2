using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.Ingredients.Commands.Delete
{
    public record DeleteIngredientCommand(Guid Id)
        : IRequest<Result>
    {
    }
}
