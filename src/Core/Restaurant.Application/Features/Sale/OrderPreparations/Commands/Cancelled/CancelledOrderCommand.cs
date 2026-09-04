using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Sale.OrderPreparations.Commands.Cancelled
{
    public record CancelledOrderCommand(Guid OrderDetailId)
        : IRequest<Result>
    {
    }
}
