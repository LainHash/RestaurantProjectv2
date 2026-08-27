using Restaurant.Application.Features.Guest.Wallets.Queries.GetByUserId;
using Restaurant.Contract.DTOs.Guest.Wallets;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Guest
{
    public interface IWalletService
    {
        Task<Result<WalletResponse>> GetByUserIdAsync(
            GetWalletByUserIdQuery query,
            CancellationToken cancellationToken = default);
    }
}
