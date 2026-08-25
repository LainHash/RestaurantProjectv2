using MediatR;
using Restaurant.Application.Services.Personnel;
using Restaurant.Contract.DTOs.Personnel.Employees;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Employees.Queries.GetById
{
    internal class GetEmployeeByIdQueryHandler(IEmployeeService employeeService)
                : IRequestHandler<GetEmployeeByIdQuery, Result<EmployeeResponse>>
    {
        private readonly IEmployeeService _employeeService = employeeService;

        public async Task<Result<EmployeeResponse>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetEmployeeByIdSpecification(request);
            var response = await _employeeService.GetByIdAsync(specification, cancellationToken);
            return response;
        }
    }
}
