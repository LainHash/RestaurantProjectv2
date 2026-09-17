using AutoMapper;
using Restaurant.Application.Features.Schedule.Reservations.Commands.Create;
using Restaurant.Application.Features.Schedule.Reservations.Queries.GetAll;
using Restaurant.Application.Features.Schedule.Reservations.Queries.GetById;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Schedule;
using Restaurant.Contract.DTOs.Schedule.Reservations;
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
        private readonly IBranchRepository _branchRepository;
        private readonly ICustomerRepository _customerRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ReservationService(
            IReservationRepository reservationRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IBranchRepository branchRepository,
            ICustomerRepository customerRepository)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _branchRepository = branchRepository;
            _customerRepository = customerRepository;
        }

        public async Task<PageResult<IEnumerable<ReservationResponse>>> GetAllAsync(
            GetAllReservationsSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var totalItems = await _reservationRepository.CountAsync(specification, cancellationToken);

            var reservations = await _reservationRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<ReservationResponse>>(reservations);
            return PageResult<IEnumerable<ReservationResponse>>
                .Succeed(response, Success<Reservation>.Retrieved, totalItems, specification.Skip, specification.Take);
        }

        public async Task<Result<ReservationDetailResponse>> GetByIdAsync(
            GetReservationByIdSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var reservation = await _reservationRepository.FindAsync(specification, cancellationToken);
            if(reservation is null)
            {
                return Result<ReservationDetailResponse>
                    .Fail(Error<Reservation>.NotFound, HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<ReservationDetailResponse>(reservation);
            return Result<ReservationDetailResponse>
                .Succeed(response, Success<Reservation>.Retrieved);
        }

        public async Task<Result<ReservationDetailResponse>> CreateAsync(
            CreateReservationCommand command,
            CreateReservationSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var branch = await _branchRepository.FindByIdAsync(command.Body.BranchId, cancellationToken);
            if(branch is null)
            {
                return Result<ReservationDetailResponse>
                    .Fail(Error<Branch>.NotFound, HttpStatusCode.NotFound);
            }

            var reservation = _mapper.Map<Reservation>(command.Body)
                .SetBranch(branch.Id);

            Customer? customer = null;
            if (command.UserId is not null)
            {
                customer = await _customerRepository
                    .FindFullPersonalProfileAsync(command.UserId.Value, cancellationToken);
                if (customer is null)
                {
                    return Result<ReservationDetailResponse>
                        .Fail(Error<Customer>.NotFound, HttpStatusCode.NotFound);
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
                .Succeed(response, Success<Reservation>.Created, HttpStatusCode.Created);
        }
    }
}
