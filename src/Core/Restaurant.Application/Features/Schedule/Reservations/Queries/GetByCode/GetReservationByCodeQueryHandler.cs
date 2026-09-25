using MediatR;
using Restaurant.Application.Services.Schedule;
using Restaurant.Contract.DTOs.Schedule.Reservations;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Schedule.Reservations.Queries.GetByCode
{
    internal class GetReservationByCodeQueryHandler(IReservationService reservationService)
                : IRequestHandler<GetReservationByCodeQuery, Result<ReservationDetailResponse>>
    {
        private readonly IReservationService _reservationService = reservationService;

        public async Task<Result<ReservationDetailResponse>> Handle(GetReservationByCodeQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetReservationByCodeSpecification(request);
            return await _reservationService.GetByCodeAsync(specification, cancellationToken);
        }
    }
}
