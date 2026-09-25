using Restaurant.Application.Features.Schedule.Reservations.Commands.Create;
using Restaurant.Application.Features.Schedule.Reservations.Queries.GetAll;
using Restaurant.Application.Features.Schedule.Reservations.Queries.GetByCode;
using Restaurant.Application.Features.Schedule.Reservations.Queries.GetById;
using Restaurant.Contract.DTOs.Schedule.Reservations;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Schedule
{
    public interface IReservationService 
    {
        Task<PageResult<IEnumerable<ReservationResponse>>> GetAllAsync(
            GetAllReservationsSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<ReservationDetailResponse>> GetByIdAsync(
            GetReservationByIdSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<ReservationDetailResponse>> GetByCodeAsync(
            GetReservationByCodeSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<ReservationDetailResponse>> CreateAsync(
            CreateReservationCommand command,
            CreateReservationSpecification specification,
            CancellationToken cancellationToken = default);


    }
}
