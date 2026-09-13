using MediatR;
using Restaurant.Application.Services.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.ChangePassword
{
    internal class ChangePasswordCommandHandler(IAuthenticationService authenticationService)
        : IRequestHandler<ChangePasswordCommand, Result>
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;

        public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            return await _authenticationService.ChangePasswordAsync(request, cancellationToken);
        }
    }
}
