using AutoMapper;
using Restaurant.Application.Features.Territory.RestaurantTables.Commands.Create;
using Restaurant.Application.Features.Territory.RestaurantTables.Commands.Update;
using Restaurant.Application.Features.Territory.RestaurantTables.Queries.GetAll;
using Restaurant.Application.Features.Territory.RestaurantTables.Queries.GetById;
using Restaurant.Application.Services.Business;
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
        private readonly IAreaRepository _areaRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RestaurantTableService(
            IRestaurantTableRepository restaurantTableRepository,
            IMapper mapper,
            IAreaRepository areaRepository,
            IUnitOfWork unitOfWork)
        {
            _restaurantTableRepository = restaurantTableRepository;
            _mapper = mapper;
            _areaRepository = areaRepository;
            _unitOfWork = unitOfWork;
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

        public async Task<Result<RestaurantTableResponse>> CreateAsync(
            CreateRestaurantTableCommand command,
            CreateRestaurantTableSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var area = await _areaRepository.FindByIdAsync(command.Body.AreaId, cancellationToken);
            if(area is null)
            {
                return Result<RestaurantTableResponse>
                    .Fail(Error<Area>.NotFound, HttpStatusCode.NotFound);
            }

            if (await _restaurantTableRepository.IsExistingTableNumberAsync(command.Body.TableNumber, cancellationToken))
            {
                return Result<RestaurantTableResponse>
                    .Fail("A table with this table number already exists.", HttpStatusCode.Conflict);
            }

            var restaurantTable = _mapper.Map<RestaurantTable>(command.Body)
                .SetArea(area.Id);

            _restaurantTableRepository.Add(restaurantTable);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            specification.ApplyCriteria(restaurantTable.Id);
            var createdRestaurantTable = await _restaurantTableRepository.FindAsync(specification, cancellationToken);

            var response = _mapper.Map<RestaurantTableResponse>(createdRestaurantTable);
            return Result<RestaurantTableResponse>
                .Succeed(response, Success<RestaurantTable>.Created, HttpStatusCode.Created);
        }

        public async Task<Result<RestaurantTableResponse>> UpdateAsync(
            UpdateRestaurantTableCommand command,
            UpdateRestaurantTableSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var area = await _areaRepository.FindByIdAsync(command.Body.AreaId, cancellationToken);
            if (area is null)
            {
                return Result<RestaurantTableResponse>
                    .Fail(Error<Area>.NotFound, HttpStatusCode.NotFound);
            }

            if (await _restaurantTableRepository.IsExistingTableNumberAsync(command.Body.TableNumber, cancellationToken))
            {
                return Result<RestaurantTableResponse>
                    .Fail("A table with this table number already exists.", HttpStatusCode.Conflict);
            }

            var restaurantTable = await _restaurantTableRepository.FindAsync(specification, cancellationToken);
            if (restaurantTable is null)
            {
                return Result<RestaurantTableResponse>
                    .Fail(Error<RestaurantTable>.NotFound, HttpStatusCode.NotFound);
            }

            _mapper.Map(command.Body, restaurantTable);
            restaurantTable.SetArea(area.Id);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<RestaurantTableResponse>(restaurantTable);
            return Result<RestaurantTableResponse>
                .Succeed(response, Success<RestaurantTable>.Updated);
        }
    }
}
