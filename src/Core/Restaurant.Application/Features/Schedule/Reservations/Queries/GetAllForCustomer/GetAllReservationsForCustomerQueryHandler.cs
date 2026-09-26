using MediatR;
using Restaurant.Application.Services.Schedule;
using Restaurant.Contract.DTOs.Schedule.Reservations;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Schedule.Reservations.Queries.GetAllForCustomer
{
    internal class GetAllReservationsForCustomerQueryHandler(IReservationService reservationService)
                : IRequestHandler<GetAllReservationsForCustomerQuery, PageResult<IEnumerable<ReservationResponse>>>
    {
        private readonly IReservationService _reservationService = reservationService;

        public async Task<PageResult<IEnumerable<ReservationResponse>>> Handle(GetAllReservationsForCustomerQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllReservationsForCustomerSpecification(request);
            return await _reservationService.GetAllForCustomerAsync(specification, cancellationToken);
        }
    }
}
