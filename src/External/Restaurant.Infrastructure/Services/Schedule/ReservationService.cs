using AutoMapper;
using Restaurant.Application.Features.Schedule.Reservations.Queries.GetAll;
using Restaurant.Application.Features.Schedule.Reservations.Queries.GetById;
using Restaurant.Application.Services.Schedule;
using Restaurant.Contract.DTOs.Schedule.Reservations;
using Restaurant.Domain.Entities.Schedule;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Schedule;
using System.Net;

namespace Restaurant.Infrastructure.Services.Schedule
{
    internal class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        private readonly IMapper _mapper;

        public ReservationService(
            IReservationRepository reservationRepository,
            IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
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
    }
}
