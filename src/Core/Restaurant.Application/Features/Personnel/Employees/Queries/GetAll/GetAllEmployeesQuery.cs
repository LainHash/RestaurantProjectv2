using MediatR;
using Restaurant.Contract.DTOs.Personnel.Employees;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Employees.Queries.GetAll
{
    public record GetAllEmployeesQuery
        : IRequest<Result<IEnumerable<EmployeeResponse>>>
    {
    }
}
