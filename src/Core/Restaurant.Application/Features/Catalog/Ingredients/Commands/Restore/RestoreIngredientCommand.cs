using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Ingredients.Commands.Restore
{
    public record RestoreIngredientCommand(Guid Id)
        : IRequest<Result>
    {
    }
}
