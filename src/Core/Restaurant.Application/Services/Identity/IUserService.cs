using Restaurant.Application.Features.Identity.Users.Commands.CreateForEmployee;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Identity
{
    public interface IUserService
    {
        Task<Result> CreateForEmployeeAsync(
            CreateUsersForEmployeeCommand command,
            CancellationToken cancellationToken = default);
    }
}
