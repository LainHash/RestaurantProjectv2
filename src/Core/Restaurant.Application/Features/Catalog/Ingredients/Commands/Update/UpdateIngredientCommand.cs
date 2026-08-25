using MediatR;
using Restaurant.Contract.DTOs.Catalog.Ingredients;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Ingredients.Commands.Update
{
    public record UpdateIngredientCommand(Guid Id, UpdateIngredientRequest Body)
        : IRequest<Result<IngredientResponse>>
    {
    }
}
