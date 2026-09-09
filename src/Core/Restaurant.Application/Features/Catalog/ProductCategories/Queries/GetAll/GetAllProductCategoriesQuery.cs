using MediatR;
using Restaurant.Contract.DTOs.Catalog.ProductCategories;
using Restaurant.Domain.Models;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.ProductCategories.Queries.GetAll
{
    public record GetAllProductCategoriesQuery
        : PageQuery, IRequest<PageResult<IEnumerable<ProductCategoryResponse>>>
    {
    }
}
