using MediatR;
using Restaurant.Contract.DTOs.Catalog.Products;
using Restaurant.Domain.Models;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetAllPopular
{
    public record GetAllPopularProductsQuery(Guid? CategoryId, Guid? BrandId, int Limit = 10)
        : PageQuery, IRequest<PageResult<IEnumerable<PopularProductResponse>>>
    {
    }
}
