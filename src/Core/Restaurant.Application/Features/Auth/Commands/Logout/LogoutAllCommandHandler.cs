using MediatR;
using Restaurant.Application.Services.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.Logout
{
    internal class LogoutAllCommandHandler(IAuthenticationService authenticationService)
        : IRequestHandler<LogoutAllCommand, Result>
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;

        public async Task<Result> Handle(LogoutAllCommand request, CancellationToken cancellationToken)
        {
            return await _authenticationService.LogoutAllAsync(request, cancellationToken);
        }
    }
}
