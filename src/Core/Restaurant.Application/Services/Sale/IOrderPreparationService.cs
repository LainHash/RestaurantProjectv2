using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Cancel;
using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Prepare;
using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Ready;
using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Serve;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Sale
{
    public interface IOrderPreparationService
    {
        Task<Result> PrepareOrderAsync(
            PrepareOrderCommand command,
            PrepareOrderSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result> ReadyOrderAsync(
            ReadyOrderCommand command,
            ReadyOrderSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result> ServeOrderAsync(
            ServeOrderCommand command,
            ServeOrderSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result> CancelOrderAsync(
            CancelOrderCommand command,
            CancelOrderSpecification specification,
            CancellationToken cancellationToken = default);
    }
}
