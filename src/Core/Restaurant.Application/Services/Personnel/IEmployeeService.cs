using Restaurant.Application.Features.Personnel.Employees.Commands.Create;
using Restaurant.Application.Features.Personnel.Employees.Queries.GetAll;
using Restaurant.Application.Features.Personnel.Employees.Queries.GetById;
using Restaurant.Contract.DTOs.Personnel.Employees;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Personnel
{
    public interface IEmployeeService
    {
        Task<Result<IEnumerable<EmployeeResponse>>> GetAllAsync(
            GetAllEmployeesSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<EmployeeResponse>> GetByIdAsync(
            GetEmployeeByIdSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<EmployeeResponse>> CreateAsync(
            CreateEmployeeCommand command,
            CancellationToken cancellationToken = default);
    }
}
