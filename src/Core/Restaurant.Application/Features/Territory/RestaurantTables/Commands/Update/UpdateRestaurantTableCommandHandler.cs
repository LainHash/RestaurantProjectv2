using MediatR;
using Restaurant.Application.Services.Territory;
using Restaurant.Contract.DTOs.Territory.RestaurantTables;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.RestaurantTables.Commands.Update
{
    internal class UpdateRestaurantTableCommandHandler(IRestaurantTableService restaurantTableService)
                : IRequestHandler<UpdateRestaurantTableCommand, Result<RestaurantTableResponse>>
    {
        private readonly IRestaurantTableService _restaurantTableService = restaurantTableService;

        public async Task<Result<RestaurantTableResponse>> Handle(UpdateRestaurantTableCommand request, CancellationToken cancellationToken)
        {
            var specification = new UpdateRestaurantTableSpecification(request);
            var response = await _restaurantTableService.UpdateAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
