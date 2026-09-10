using MediatR;
using Restaurant.Application.Services.Territory;
using Restaurant.Contract.DTOs.Territory.RestaurantTables;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.RestaurantTables.Queries.GetAllByAreaId
{
    internal class GetAllRestaurantTableByAreaIdQueryHandler(IRestaurantTableService restaurantTableService)
                : IRequestHandler<GetAllRestaurantTableByAreaIdQuery, Result<IEnumerable<RestaurantTableResponse>>>
    {
        private readonly IRestaurantTableService _restaurantTableService = restaurantTableService;

        public async Task<Result<IEnumerable<RestaurantTableResponse>>> Handle(GetAllRestaurantTableByAreaIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllRestaurantTableByAreaIdSpecification(request);
            var response = await _restaurantTableService.GetAllByAreaIdAsync(specification, cancellationToken);
            return response;

        }
    }
}
