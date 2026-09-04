using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Sale.OrderPreparations.Commands.Preparing
{
    public record PreparingOrderCommand(Guid OrderDetailId)
        : IRequest<Result>
    {
    }
}
