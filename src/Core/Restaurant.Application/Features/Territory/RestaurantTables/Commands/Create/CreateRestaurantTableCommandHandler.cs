using MediatR;
using Restaurant.Application.Services.Territory;
using Restaurant.Contract.DTOs.Territory.RestaurantTables;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.RestaurantTables.Commands.Create
{
    internal class CreateRestaurantTableCommandHandler(IRestaurantTableService restaurantTableService)
                : IRequestHandler<CreateRestaurantTableCommand, Result<RestaurantTableResponse>>
    {
        private readonly IRestaurantTableService _restaurantTableService = restaurantTableService;

        public async Task<Result<RestaurantTableResponse>> Handle(CreateRestaurantTableCommand request, CancellationToken cancellationToken)
        {
            var specification = new CreateRestaurantTableSpecification();
            var response = await _restaurantTableService.CreateAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
