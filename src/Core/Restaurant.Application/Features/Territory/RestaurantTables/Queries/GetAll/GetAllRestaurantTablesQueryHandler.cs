using MediatR;
using Restaurant.Application.Services.Territory;
using Restaurant.Contract.DTOs.Territory.RestaurantTables;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.RestaurantTables.Queries.GetAll
{
    internal class GetAllRestaurantTablesQueryHandler(IRestaurantTableService restaurantTableService)
                : IRequestHandler<GetAllRestaurantTablesQuery, PageResult<IEnumerable<RestaurantTableResponse>>>
    {
        private readonly IRestaurantTableService _restaurantTableService = restaurantTableService;

        public async Task<PageResult<IEnumerable<RestaurantTableResponse>>> Handle(GetAllRestaurantTablesQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllRestaurantTablesSpecification(request);
            var response = await _restaurantTableService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}
