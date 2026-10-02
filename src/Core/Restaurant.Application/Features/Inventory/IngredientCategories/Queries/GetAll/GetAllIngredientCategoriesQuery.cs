using MediatR;
using Restaurant.Contract.DTOs.Inventory.IngredientCategories;
using Restaurant.Domain.Models;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Inventory.IngredientCategories.Queries.GetAll
{
    public record GetAllIngredientCategoriesQuery
        : PageQuery, IRequest<PageResult<IEnumerable<IngredientCategoryResponse>>>
    {
    }
}
