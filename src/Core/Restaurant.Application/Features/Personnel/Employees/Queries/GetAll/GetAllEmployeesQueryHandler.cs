using MediatR;
using Restaurant.Application.Services.Personnel;
using Restaurant.Contract.DTOs.Personnel.Employees;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Employees.Queries.GetAll
{
    internal class GetAllEmployeesQueryHandler(IEmployeeService employeeService)
                : IRequestHandler<GetAllEmployeesQuery, Result<IEnumerable<EmployeeResponse>>>
    {
        private readonly IEmployeeService _employeeService = employeeService;

        public async Task<Result<IEnumerable<EmployeeResponse>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllEmployeesSpecification(request);
            var response = await _employeeService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}
