using MediatR;
using Restaurant.Contract.DTOs.Identity.Roles;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.Roles.Commands.Update
{
    public record UpdateRoleCommand(Guid Id, UpdateRoleRequest Body)
        : IRequest<Result<RoleResponse>>
    {
    }
}
