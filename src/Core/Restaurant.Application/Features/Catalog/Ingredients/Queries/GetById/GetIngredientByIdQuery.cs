using MediatR;
using Restaurant.Contract.DTOs.Catalog.Ingredients;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Ingredients.Queries.GetById
{
    public record GetIngredientByIdQuery(Guid Id)
        : IRequest<Result<IngredientResponse>>
    {
    }
}
