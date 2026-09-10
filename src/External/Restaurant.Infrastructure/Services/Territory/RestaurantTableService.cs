using AutoMapper;
using Restaurant.Application.Features.Territory.RestaurantTables.Queries.GetAll;
using Restaurant.Application.Features.Territory.RestaurantTables.Queries.GetById;
using Restaurant.Application.Services.Territory;
using Restaurant.Contract.DTOs.Territory.RestaurantTables;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Territory;
using System.Net;

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

        public async Task<Result<RestaurantTableResponse>> GetByIdAsync(
            GetRestaurantTableByIdSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var restaurantTable = await _restaurantTableRepository.FindAsync(specification, cancellationToken);
            if(restaurantTable is null)
            {
                return Result<RestaurantTableResponse>
                    .Fail(Error<RestaurantTable>.NotFound, HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<RestaurantTableResponse>(restaurantTable);
            return Result<RestaurantTableResponse>
                .Succeed(response, Success<RestaurantTable>.Retrieved);
        }
    }
}
