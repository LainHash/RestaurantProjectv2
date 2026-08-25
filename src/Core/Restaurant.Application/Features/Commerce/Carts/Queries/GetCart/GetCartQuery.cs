using MediatR;
using Restaurant.Contract.DTOs.Commerce.Carts;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Commerce.Carts.Queries.GetCart
{
    public record GetCartQuery(Guid? UserId, string? SessionId)
        : IRequest<Result<CartResponse>>
    {
    }
}
