using AutoMapper;
using Restaurant.Application.Features.Guest.Customers.Queries.GetAll;
using Restaurant.Application.Features.Guest.Customers.Queries.GetById;
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

        private readonly IWalletService _walletService;

        private readonly IMapper _mapper;

        public CustomerService(
            ICustomerRepository customerRepository,
            IMapper mapper,
            IWalletService walletService)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _walletService = walletService;
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

        public async Task<Result<CustomerResponse>> GetByIdAsync(
            GetCustomerByIdSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var customer = await _customerRepository.FindAsync(specification, cancellationToken);
            if (customer is null)
            {
                return Result<CustomerResponse>
                    .Fail(Error.NotFound("Customer"), HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<CustomerResponse>(customer);
            return Result<CustomerResponse>
                .Succeed(response, Success.Retrieved("Customer"));
        }

    }
}
