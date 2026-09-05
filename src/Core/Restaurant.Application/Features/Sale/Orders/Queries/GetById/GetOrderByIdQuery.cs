using MediatR;
using Restaurant.Contract.DTOs.Sale.Orders;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Sale.Orders.Queries.GetById
{
    public record GetOrderByIdQuery(Guid Id)
        : IRequest<Result<OrderResponse>>
    {
    }
}
