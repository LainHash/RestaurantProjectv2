using MediatR;
using Restaurant.Application.DTOs.Identity.PersonalProfiles;
using Restaurant.Application.DTOs.Personnel.Employees;
using Restaurant.Contract.DTOs.Personnel.Employees;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Employees.Commands.Create
{
    public record CreateEmployeeCommand(
        CreateEmployeeRequest CreateEmployeeRequest,
        CreatePersonalProfileRequest CreatePersonalProfileRequest)
        : IRequest<Result<EmployeeResponse>>
    {
    }
}
