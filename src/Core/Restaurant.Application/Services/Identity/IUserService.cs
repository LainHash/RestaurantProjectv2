using Restaurant.Application.Features.Identity.Users.Commands.CreateForEmployee;
using Restaurant.Application.Features.Identity.Users.Commands.ResetPassword;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Identity
{
    public interface IUserService
    {
        Task<Result> CreateForEmployeeAsync(
            CreateUsersForEmployeeCommand command,
            CancellationToken cancellationToken = default);

        Task<Result> ResetPasswordAsync(
            ResetPasswordCommand command,
            CancellationToken cancellationToken = default);
    }
}
