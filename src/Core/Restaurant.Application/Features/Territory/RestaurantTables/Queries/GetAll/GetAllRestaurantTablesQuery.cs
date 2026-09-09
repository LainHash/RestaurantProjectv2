using MediatR;
using Restaurant.Contract.DTOs.Territory.RestaurantTables;
using Restaurant.Domain.Models;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.RestaurantTables.Queries.GetAll
{
    public record GetAllRestaurantTablesQuery
        : PageQuery, IRequest<PageResult<IEnumerable<RestaurantTableResponse>>>
    {
    }
}
