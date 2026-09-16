using MediatR;
using Restaurant.Application.Services.Schedule;
using Restaurant.Contract.DTOs.Schedule.Reservations;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Schedule.Reservations.Queries.GetAll
{
    internal class GetAllReservationsQueryHandler(IReservationService reservationService)
                : IRequestHandler<GetAllReservationsQuery, PageResult<IEnumerable<ReservationResponse>>>
    {
        private readonly IReservationService _reservationService = reservationService;

        public async Task<PageResult<IEnumerable<ReservationResponse>>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllReservationsSpecification(request);
            var response = await _reservationService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}
