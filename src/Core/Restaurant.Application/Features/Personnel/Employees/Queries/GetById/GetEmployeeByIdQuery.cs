using MediatR;
using Restaurant.Contract.DTOs.Personnel.Employees;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Employees.Queries.GetById
{
    public record GetEmployeeByIdQuery(Guid Id)
        : IRequest<Result<EmployeeResponse>>
    {
    }
}
