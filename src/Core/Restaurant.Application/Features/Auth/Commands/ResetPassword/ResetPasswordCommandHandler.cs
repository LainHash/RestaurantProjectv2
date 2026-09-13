using MediatR;
using Restaurant.Application.Services.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.ResetPassword
{
    internal class ResetPasswordCommandHandler(IAuthenticationService authenticationService)
                : IRequestHandler<ResetPasswordCommand, Result>
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;

        public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var response = await _authenticationService.ResetPasswordAsync(request, cancellationToken);
            return response;
        }
    }
}
