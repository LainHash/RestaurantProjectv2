using MediatR;
using Restaurant.Contract.DTOs.Schedule.Reservations;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Schedule.Reservations.Commands.UpdateByCustomer
{
    public record UpdateReservationByCustomerCommand(Guid Id, UpdateReservationByCustomerRequest Body)
        : IRequest<Result<ReservationMinimalResponse>>
    {
    }
}
