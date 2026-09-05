using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Sale.OrderPreparations.Commands.Prepare
{
    public record PrepareOrderCommand(Guid OrderDetailId)
        : IRequest<Result>
    {
    }
}
