using MediatR;
using Restaurant.Application.Services.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.Logout
{
    internal class LogoutCommandHandler(IAuthenticationService authenticationService)
        : IRequestHandler<LogoutCommand, Result>
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;

        public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            return await _authenticationService.LogoutAsync(request, cancellationToken);
        }
    }
}
