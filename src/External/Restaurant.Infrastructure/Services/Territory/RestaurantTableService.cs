using AutoMapper;
using Restaurant.Application.Features.Territory.RestaurantTables.Queries.GetAll;
using Restaurant.Application.Services.Territory;
using Restaurant.Contract.DTOs.Territory.RestaurantTables;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Territory;

namespace Restaurant.Infrastructure.Services.Territory
{
    internal class RestaurantTableService : IRestaurantTableService
    {
        private readonly IRestaurantTableRepository _restaurantTableRepository;

        private readonly IMapper _mapper;

        public RestaurantTableService(
            IRestaurantTableRepository restaurantTableRepository,
            IMapper mapper)
        {
            _restaurantTableRepository = restaurantTableRepository;
            _mapper = mapper;
        }

        public async Task<PageResult<IEnumerable<RestaurantTableResponse>>> GetAllAsync(
            GetAllRestaurantTablesSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var totalItems = await _restaurantTableRepository.CountAsync(specification, cancellationToken);

            var restaurantTables = await _restaurantTableRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<RestaurantTableResponse>>(restaurantTables);
            return PageResult<IEnumerable<RestaurantTableResponse>>
                .Succeed(response, Success<RestaurantTable>.Retrieved, totalItems, specification.Skip, specification.Take);
        }
    }
}
