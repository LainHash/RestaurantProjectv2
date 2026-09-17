using AutoMapper;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Guest;
using Restaurant.Contract.DTOs.Guest.Wallets;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Guest;
using Restaurant.Domain.Repositories.Identity;
using System.Net;

namespace Restaurant.Infrastructure.Services.Guest
{
    internal class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public WalletService(
            IWalletRepository walletRepository,
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            ICustomerRepository customerRepository,
            IMapper mapper)
        {
            _walletRepository = walletRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        private async Task<Wallet> InitializeAsync(
            Func<Wallet> factory,
            CancellationToken cancellationToken = default)
        {
            var wallet = factory();

            _walletRepository.Add(wallet);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return wallet;
        }

        private async Task<Wallet> GetOrCreateAsync(
            long customerId,
            Func<Wallet> factory,
            CancellationToken cancellationToken = default)
        {
            var wallet = await _walletRepository.FindByCustomerIdAsync(customerId, cancellationToken);

            if(wallet is not null)
            {
                return wallet;
            }

            return await InitializeAsync(factory, cancellationToken);
        }
    }
}
