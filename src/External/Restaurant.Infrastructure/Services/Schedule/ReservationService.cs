using AutoMapper;
using Restaurant.Application.Features.Schedule.Reservations.Commands.Create;
using Restaurant.Application.Features.Schedule.Reservations.Queries.GetAll;
using Restaurant.Application.Features.Schedule.Reservations.Queries.GetByCode;
using Restaurant.Application.Features.Schedule.Reservations.Queries.GetById;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Schedule;
using Restaurant.Contract.DTOs.Schedule.Reservations;
using Restaurant.Contract.DTOs.Schedule.ReservationTables;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Entities.Schedule;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Guest;
using Restaurant.Domain.Repositories.Schedule;
using Restaurant.Domain.Repositories.Territory;
using System.Net;

namespace Restaurant.Infrastructure.Services.Schedule
{
    internal class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRestaurantTableRepository _restaurantTableRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly ICustomerRepository _customerRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ReservationService(
            IReservationRepository reservationRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IBranchRepository branchRepository,
            ICustomerRepository customerRepository,
            IRestaurantTableRepository restaurantTableRepository)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _branchRepository = branchRepository;
            _customerRepository = customerRepository;
            _restaurantTableRepository = restaurantTableRepository;
        }

        public async Task<PageResult<IEnumerable<ReservationResponse>>> GetAllAsync(
            GetAllReservationsSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var totalItems = await _reservationRepository.CountAsync(specification, cancellationToken);

            var reservations = await _reservationRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<ReservationResponse>>(reservations);
            return PageResult<IEnumerable<ReservationResponse>>
                .Succeed(response, Success.Retrieved("Reservation"), totalItems, specification.Skip, specification.Take);
        }

        public async Task<Result<ReservationDetailResponse>> GetByIdAsync(
            GetReservationByIdSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var reservation = await _reservationRepository.FindAsync(specification, cancellationToken);
            if(reservation is null)
            {
                return Result<ReservationDetailResponse>
                    .Fail(Error.NotFound("Reservation"), HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<ReservationDetailResponse>(reservation);
            return Result<ReservationDetailResponse>
                .Succeed(response, Success.Retrieved("Reservation"));
        }

        public async Task<Result<ReservationDetailResponse>> GetByCodeAsync(
            GetReservationByCodeSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var reservation = await _reservationRepository.FindAsync(specification, cancellationToken);
            if (reservation is null)
            {
                return Result<ReservationDetailResponse>
                    .Fail(Error.NotFound("Reservation"), HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<ReservationDetailResponse>(reservation);
            return Result<ReservationDetailResponse>
                .Succeed(response, Success.Retrieved("Reservation"));
        }

        public async Task<Result<ReservationDetailResponse>> CreateAsync(
            CreateReservationCommand command,
            CreateReservationSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var branch = await _branchRepository
                .FindByIdAsync(command.Body.BranchPublicId, cancellationToken);
            if(branch is null)
            {
                return Result<ReservationDetailResponse>
                    .Fail(Error.NotFound("Branch"), HttpStatusCode.NotFound);
            }

            var reservationTablesResult = await ResolveReservationTablesAsync(
                command.Body.ReservationTables,
                branch.Id,
                cancellationToken);

            if (!reservationTablesResult.IsSucceed)
            {
                return Result<ReservationDetailResponse>
                    .Fail(reservationTablesResult.Message, (HttpStatusCode)reservationTablesResult.StatusCode);
            }

            var reservation = _mapper.Map<Reservation>(command.Body)
                .SetBranch(branch.Id)
                .SetReservationTables(reservationTablesResult.Data!);

            Customer? customer = null;
            if (command.UserId is not null)
            {
                customer = await _customerRepository
                    .FindFullPersonalProfileAsync(command.UserId.Value, cancellationToken);
                if (customer is null)
                {
                    return Result<ReservationDetailResponse>
                        .Fail(Error.NotFound("Customer"), HttpStatusCode.NotFound);
                }
            }

            if (customer is not null)
            {
                reservation.SetGuest(customer);
            }

            _reservationRepository.Add(reservation);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            specification.ApplyCriteria(reservation.Id);

            var createdReservation = await _reservationRepository
                .FindAsync(specification, cancellationToken);

            var response = _mapper.Map<ReservationDetailResponse>(createdReservation);
            return Result<ReservationDetailResponse>
                .Succeed(response, Success.Created("Reservation"), HttpStatusCode.Created);
        }

        private async Task<Result<List<ReservationTable>>> ResolveReservationTablesAsync(
            IEnumerable<CreateReservationTableRequest>? tableRequests,
            long branchId,
            CancellationToken cancellationToken)
        {
            var reservationTables = new List<ReservationTable>();

            if (tableRequests is null || !tableRequests.Any())
            {
                return Result<List<ReservationTable>>
                    .Succeed(reservationTables, string.Empty);
            }

            foreach (var tableRequest in tableRequests)
            {
                var table = await _restaurantTableRepository
                    .FindByIdAsync(tableRequest.RestaurantTablePublicId, cancellationToken);

                if (table is null)
                {
                    return Result<List<ReservationTable>>
                        .Fail(Error.NotFound("RestaurantTable"), HttpStatusCode.NotFound);
                }

                if (table.Area is null || table.Area.BranchId != branchId)
                {
                    return Result<List<ReservationTable>>
                        .Fail("Restaurant table does not belong to the selected branch.", HttpStatusCode.BadRequest);
                }

                reservationTables.Add(new ReservationTable(table.Id));
            }

            return Result<List<ReservationTable>>
                .Succeed(reservationTables, string.Empty);
        }
    }
}
