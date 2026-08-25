using MediatR;
using Restaurant.Contract.DTOs.Guest.Customers;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Guest.Customers.Queries.GetByUserId
{
    public record GetCustomerByUserIdQuery(Guid UserId)
        : IRequest<Result<CustomerResponse>>
    {
    }
}
