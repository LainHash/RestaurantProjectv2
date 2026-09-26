using MediatR;
using Restaurant.Application.Services.Schedule;
using Restaurant.Contract.DTOs.Schedule.Reservations;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Schedule.Reservations.Queries.GetAllByCustomer
{
    internal class GetAllReservationsByCustomerQueryHandler(IReservationService reservationService)
                : IRequestHandler<GetAllReservationsByCustomerQuery, PageResult<IEnumerable<ReservationMinimalResponse>>>
    {
        private readonly IReservationService _reservationService = reservationService;

        public async Task<PageResult<IEnumerable<ReservationMinimalResponse>>> Handle(GetAllReservationsByCustomerQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllReservationsByCustomerSpecification(request);
            return await _reservationService.GetAllByCustomerAsync(request, specification, cancellationToken);
        }
    }
}
