using MediatR;
using Restaurant.Contract.DTOs.Guest.Wallets;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Guest.Wallets.Queries.GetByUserId
{
    public record GetWalletByUserIdQuery(Guid UserId)
        : IRequest<Result<WalletResponse>>
    {
    }
}
