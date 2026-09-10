using MediatR;
using Restaurant.Contract.DTOs.Territory.RestaurantTables;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.RestaurantTables.Commands.Create
{
    public record CreateRestaurantTableCommand(CreateRestaurantTableRequest Body)
        : IRequest<Result<RestaurantTableResponse>>
    {
    }
}
