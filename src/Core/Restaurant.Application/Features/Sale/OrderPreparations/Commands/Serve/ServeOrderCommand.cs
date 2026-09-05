using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Sale.OrderPreparations.Commands.Serve
{
    public record ServeOrderCommand(Guid OrderDetailId)
        : IRequest<Result>
    {
    }
}
