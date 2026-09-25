using MediatR;
using Restaurant.Contract.DTOs.Schedule.Reservations;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Schedule.Reservations.Queries.GetByCode
{
    public record GetReservationByCodeQuery(string Code)
        : IRequest<Result<ReservationDetailResponse>>
    {
    }
}
