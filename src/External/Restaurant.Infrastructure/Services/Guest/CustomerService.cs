using AutoMapper;
using Restaurant.Application.Features.Guest.Customers.Queries.GetAll;
using Restaurant.Application.Features.Guest.Customers.Queries.GetById;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Guest;
using Restaurant.Contract.DTOs.Guest.Customers;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Guest;
using System.Net;

namespace Restaurant.Infrastructure.Services.Guest
{
    internal class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IWalletRepository _walletRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(
            ICustomerRepository customerRepository,
            IMapper mapper,
            IWalletRepository walletRepository,
            IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _walletRepository = walletRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<CustomerResponse>>> GetAllAsync(
            GetAllCustomersSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var customers = await _customerRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<CustomerResponse>>(customers);
            return Result<IEnumerable<CustomerResponse>>
                .Succeed(response, Success.Retrieved("Customer"));
        }

        public async Task<Result<CustomerDetailResponse>> GetByIdAsync(
            GetCustomerByIdSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var customer = await _customerRepository.FindAsync(specification, cancellationToken);
            if (customer is null)
            {
                return Result<CustomerDetailResponse>
                    .Fail(Error.NotFound("Customer"), HttpStatusCode.NotFound);
            }

            if (customer.Wallet is null)
            {
                await WalletInitializeAsync(() => new Wallet(customer.Id), cancellationToken);
            }

            var response = _mapper.Map<CustomerDetailResponse>(customer);
            return Result<CustomerDetailResponse>
                .Succeed(response, Success.Retrieved("Customer"));
        }

        private async Task WalletInitializeAsync(Func<Wallet> factory, CancellationToken cancellationToken)
        {
            var wallet = factory();
            _walletRepository.Add(wallet);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

    }
}
