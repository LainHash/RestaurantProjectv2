using Restaurant.Application.Features.Guest.Customers.Queries.GetAll;
using Restaurant.Application.Features.Guest.Customers.Queries.GetById;
using Restaurant.Contract.DTOs.Guest.Customers;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Guest
{
    public interface ICustomerService
    {
        Task<Result<IEnumerable<CustomerResponse>>> GetAllAsync(
            GetAllCustomersSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<CustomerDetailResponse>> GetByIdAsync(
            GetCustomerByIdSpecification specification,
            CancellationToken cancellationToken = default);
    }
}
