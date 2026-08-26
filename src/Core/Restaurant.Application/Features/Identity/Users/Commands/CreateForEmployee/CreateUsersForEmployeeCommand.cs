using MediatR;
using Restaurant.Application.DTOs.Identity.Users;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.Users.Commands.CreateForEmployee
{
    public record CreateUsersForEmployeeCommand(CreateUsersForEmployeeRequest Body)
        : IRequest<Result>
    {
    }
}
