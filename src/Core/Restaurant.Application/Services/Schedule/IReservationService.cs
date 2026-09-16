using Restaurant.Application.Features.Schedule.Reservations.Queries.GetAll;
using Restaurant.Contract.DTOs.Schedule.Reservations;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Schedule
{
    public interface IReservationService 
    {
        Task<PageResult<IEnumerable<ReservationResponse>>> GetAllAsync(
            GetAllReservationsSpecification specification,
            CancellationToken cancellationToken = default);
    }
}
