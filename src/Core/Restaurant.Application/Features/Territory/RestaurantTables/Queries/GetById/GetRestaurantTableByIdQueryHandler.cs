using MediatR;
using Restaurant.Application.Services.Territory;
using Restaurant.Contract.DTOs.Territory.RestaurantTables;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.RestaurantTables.Queries.GetById
{
    internal class GetRestaurantTableByIdQueryHandler(IRestaurantTableService restaurantTableService)
                : IRequestHandler<GetRestaurantTableByIdQuery, Result<RestaurantTableResponse>>
    {
        private readonly IRestaurantTableService _restaurantTableService = restaurantTableService;

        public async Task<Result<RestaurantTableResponse>> Handle(GetRestaurantTableByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetRestaurantTableByIdSpecification(request);
            var response = await _restaurantTableService.GetByIdAsync(specification, cancellationToken);
            return response;
        }
    }
}
