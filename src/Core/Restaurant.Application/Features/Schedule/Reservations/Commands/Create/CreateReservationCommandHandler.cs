using MediatR;
using Restaurant.Application.Services.Schedule;
using Restaurant.Contract.DTOs.Schedule.Reservations;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Schedule.Reservations.Commands.Create
{
    internal class CreateReservationCommandHandler(IReservationService reservationService)
                : IRequestHandler<CreateReservationCommand, Result<ReservationDetailResponse>>
    {
        private readonly IReservationService _reservationService = reservationService;

        public async Task<Result<ReservationDetailResponse>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var specification = new CreateReservationSpecification();
            var response = await _reservationService.CreateAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
