using MediatR;
using Restaurant.Contract.DTOs.Territory.RestaurantTables;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.RestaurantTables.Commands.Update
{
    public record UpdateRestaurantTableCommand(Guid Id, UpdateRestaurantTableRequest Body)
        : IRequest<Result<RestaurantTableResponse>>
    {
    }
}
