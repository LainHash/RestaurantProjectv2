using MediatR;
using Restaurant.Contract.DTOs.Schedule.Reservations;
using Restaurant.Domain.Models;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Schedule.Reservations.Queries.GetAllByCustomer
{
    public record GetAllReservationsByCustomerQuery(Guid UserId, DateTime? FromDate, DateTime? ToDate)
        : PageQuery, IRequest<PageResult<IEnumerable<ReservationMinimalResponse>>>
    {
    }
}
