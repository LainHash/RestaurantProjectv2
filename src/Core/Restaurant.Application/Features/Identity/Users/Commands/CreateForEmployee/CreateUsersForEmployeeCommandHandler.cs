using MediatR;
using Restaurant.Application.Services.Identity;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.Users.Commands.CreateForEmployee
{
    internal class CreateUsersForEmployeeCommandHandler(IUserService userService)
                : IRequestHandler<CreateUsersForEmployeeCommand, Result>
    {
        private readonly IUserService _userService = userService;

        public async Task<Result> Handle(CreateUsersForEmployeeCommand request, CancellationToken cancellationToken)
        {
            var response = await _userService.CreateForEmployeeAsync(request, cancellationToken);
            return response;
        }
    }
}
