using MediatR;
using Restaurant.Contract.DTOs.Schedule.Reservations;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Schedule.Reservations.Queries.GetById
{
    public record GetReservationByIdQuery(Guid Id)
        : IRequest<Result<ReservationDetailResponse>>
    {
    }
}
