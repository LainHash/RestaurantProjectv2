using MediatR;
using Restaurant.Application.Services.Guest;
using Restaurant.Contract.DTOs.Guest.Wallets;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Guest.Wallets.Queries.GetByUserId
{
    internal class GetWalletByUserIdQueryHandler(IWalletService walletService)
                : IRequestHandler<GetWalletByUserIdQuery, Result<WalletResponse>>
    {
        private readonly IWalletService _walletService = walletService;

        public async Task<Result<WalletResponse>> Handle(GetWalletByUserIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _walletService.GetByUserIdAsync(request, cancellationToken);
            return response;
        }
    }
}
