using Restaurant.Application.Features.Identity.Users.Commands.CreateForEmployee;
using Restaurant.Application.Features.Identity.Users.Queries.GetAll;
using Restaurant.Application.Features.Identity.Users.Queries.GetById;
using Restaurant.Contract.DTOs.Identity.Users;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Identity
{
    public interface IUserService
    {
        Task<PageResult<IEnumerable<UserResponse>>> GetAllAsync(
            GetAllUsersSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<UserDetailResponse>> GetByIdAsync(
            GetUserByIdSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result> CreateForEmployeeAsync(
            CreateUsersForEmployeeCommand command,
            CancellationToken cancellationToken = default);
    }
}
