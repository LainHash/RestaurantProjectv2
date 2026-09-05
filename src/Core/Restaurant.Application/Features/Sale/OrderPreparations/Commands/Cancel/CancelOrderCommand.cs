using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Sale.OrderPreparations.Commands.Cancel
{
    public record CancelOrderCommand(Guid OrderDetailId)
        : IRequest<Result>
    {
    }
}
