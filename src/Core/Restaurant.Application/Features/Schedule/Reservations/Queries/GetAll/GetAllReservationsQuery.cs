using MediatR;
using Restaurant.Contract.DTOs.Schedule.Reservations;
using Restaurant.Domain.Models;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Schedule.Reservations.Queries.GetAll
{
    public record GetAllReservationsQuery(DateTime? FromDate, DateTime? ToDate)
        : PageQuery, IRequest<PageResult<IEnumerable<ReservationResponse>>>
    {
    }
}
