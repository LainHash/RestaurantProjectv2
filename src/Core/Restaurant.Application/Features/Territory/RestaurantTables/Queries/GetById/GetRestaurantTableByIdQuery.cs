using MediatR;
using Restaurant.Contract.DTOs.Territory.RestaurantTables;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.RestaurantTables.Queries.GetById
{
    public record GetRestaurantTableByIdQuery(Guid Id)
        : IRequest<Result<RestaurantTableResponse>>
    {
    }
}
