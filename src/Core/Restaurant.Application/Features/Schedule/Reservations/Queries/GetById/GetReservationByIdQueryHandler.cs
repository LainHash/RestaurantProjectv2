using MediatR;
using Restaurant.Application.Services.Schedule;
using Restaurant.Contract.DTOs.Schedule.Reservations;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Schedule.Reservations.Queries.GetById
{
    internal class GetReservationByIdQueryHandler(IReservationService reservationService)
                : IRequestHandler<GetReservationByIdQuery, Result<ReservationDetailResponse>>
    {
        private readonly IReservationService _reservationService = reservationService;

        public async Task<Result<ReservationDetailResponse>> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetReservationByIdSpecification(request);
            var response = await _reservationService.GetByIdAsync(specification, cancellationToken);
            return response;
        }
    }
}
