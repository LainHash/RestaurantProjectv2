using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Sale.OrderPreparations.Commands.Served
{
    public record ServedOrderCommand(Guid OrderDetailId)
        : IRequest<Result>
    {
    }
}
