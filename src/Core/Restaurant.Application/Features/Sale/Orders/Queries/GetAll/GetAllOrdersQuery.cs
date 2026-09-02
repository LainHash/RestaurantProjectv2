using MediatR;
using Restaurant.Contract.DTOs.Sale.Orders;
using Restaurant.Domain.Models;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Sale.Orders.Queries.GetAll
{
    public record GetAllOrdersQuery(
        string? CustomerCode,
        string? EmployeeCode,
        string? BranchCode)
        : PageQuery, IRequest<PageResult<IEnumerable<OrderResponse>>>
    {
    }
}
