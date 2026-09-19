using MediatR;
using Restaurant.Contract.DTOs.Schedule.Reservations;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Schedule.Reservations.Commands.Create
{
    public record CreateReservationCommand(Guid? UserId, CreateReservationRequest Body)
        : IRequest<Result<ReservationDetailResponse>>
    {
    }
}
