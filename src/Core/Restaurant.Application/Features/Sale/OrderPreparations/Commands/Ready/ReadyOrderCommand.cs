using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Sale.OrderPreparations.Commands.Ready
{
    public record ReadyOrderCommand(Guid OrderDetailId)
        : IRequest<Result>
    {
    }
}
