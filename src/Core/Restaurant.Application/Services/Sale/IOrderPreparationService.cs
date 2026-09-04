using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Preparing;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Sale
{
    public interface IOrderPreparationService
    {
        Task<Result> PreparingOrderAsync(
            PreparingOrderCommand command,
            PreparingOrderSpecification specification,
            CancellationToken cancellationToken = default);
    }
}
