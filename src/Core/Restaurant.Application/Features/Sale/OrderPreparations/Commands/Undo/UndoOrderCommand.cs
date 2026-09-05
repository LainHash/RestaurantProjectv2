using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Sale.OrderPreparations.Commands.Undo
{
    public record UndoOrderCommand(Guid OrderDetailId)
        : IRequest<Result>
    {
    }
}
