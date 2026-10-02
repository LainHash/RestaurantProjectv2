using MediatR;
using Restaurant.Application.Services.Schedule;
using Restaurant.Contract.DTOs.Schedule.Reservations;
using Restaurant.Domain.Models.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Application.Features.Schedule.Reservations.Commands.UpdateByCustomer
{
    internal class UpdateReservationByCustomerCommandHandler(IReservationService reservationService)
                : IRequestHandler<UpdateReservationByCustomerCommand, Result<ReservationMinimalResponse>>
    {
        private readonly IReservationService _reservationService = reservationService;

        public async Task<Result<ReservationMinimalResponse>> Handle(UpdateReservationByCustomerCommand request, CancellationToken cancellationToken)
        {
            var specification = new UpdateReservationByCustomerSpecification(request);
            return await _reservationService.UpdateByCustomerAsync(request, specification, cancellationToken);
        }
    }
}
