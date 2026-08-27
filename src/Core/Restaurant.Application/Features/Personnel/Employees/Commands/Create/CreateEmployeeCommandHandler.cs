using MediatR;
using Restaurant.Application.Services.Personnel;
using Restaurant.Contract.DTOs.Personnel.Employees;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Employees.Commands.Create
{
    internal class CreateEmployeeCommandHandler(IEmployeeService employeeService)
                : IRequestHandler<CreateEmployeeCommand, Result<EmployeeResponse>>
    {
        private readonly IEmployeeService _employeeService = employeeService;

        public async Task<Result<EmployeeResponse>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var specification = new CreateEmployeeSpecification(request);
            var response = await _employeeService.CreateAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
