using MediatR;
using Restaurant.Contract.DTOs.Inventory.IngredientCategories;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.IngredientCategories.Queries.GetById
{
    public record GetIngredientCategoryByIdQuery(Guid Id)
        : IRequest<Result<IngredientCategoryResponse>>
    {
    }
}
