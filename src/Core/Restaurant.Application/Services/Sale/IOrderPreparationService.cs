using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Cancelled;
using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Preparing;
using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Ready;
using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Served;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Sale
{
    public interface IOrderPreparationService
    {
        Task<Result> PreparingOrderAsync(
            PreparingOrderCommand command,
            PreparingOrderSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result> ReadyOrderAsync(
            ReadyOrderCommand command,
            ReadyOrderSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result> ServedOrderAsync(
            ServedOrderCommand command,
            ServedOrderSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result> CancelledOrderAsync(
            CancelledOrderCommand command,
            CancelledOrderSpecification specification,
            CancellationToken cancellationToken = default);
    }
}
