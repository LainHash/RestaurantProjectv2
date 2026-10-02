using MediatR;
using Restaurant.Contract.DTOs.Catalog.Ingredients;
using Restaurant.Domain.Models;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.Ingredients.Queries.GetAll
{
    public record GetAllIngredientsQuery(Guid? CategoryId, Guid? BrandId)
        : PageQuery, IRequest<PageResult<IEnumerable<IngredientResponse>>>
    {
    }
}
