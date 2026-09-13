using MediatR;
using Restaurant.Contract.DTOs.Identity.Users;
using Restaurant.Domain.Models;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.Users.Queries.GetAll
{
    public record GetAllUsersQuery(Guid? RoleId)
        : PageQuery, IRequest<PageResult<IEnumerable<UserResponse>>>
    {
    }
}
