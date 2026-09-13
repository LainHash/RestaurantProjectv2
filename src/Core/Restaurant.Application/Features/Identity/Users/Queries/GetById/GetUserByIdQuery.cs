using MediatR;
using Restaurant.Contract.DTOs.Identity.Users;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.Users.Queries.GetById
{
    public record GetUserByIdQuery(Guid Id)
        : IRequest<Result<UserDetailResponse>>
    {
    }
}
