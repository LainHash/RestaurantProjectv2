using MediatR;
using Restaurant.Contract.DTOs.Territory.RestaurantTables;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.RestaurantTables.Queries.GetAllByAreaId
{
    public record GetAllRestaurantTableByAreaIdQuery(Guid AreaId)
        : IRequest<Result<IEnumerable<RestaurantTableResponse>>>
    {
    }
}
