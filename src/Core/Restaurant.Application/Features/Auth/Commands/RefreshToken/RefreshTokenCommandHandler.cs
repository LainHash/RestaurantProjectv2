using MediatR;
using Restaurant.Application.Services.Auth;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.RefreshToken
{
    internal class RefreshTokenCommandHandler(IAuthenticationService authenticationService)
        : IRequestHandler<RefreshTokenCommand, Result<AuthenticationResponse>>
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;

        public async Task<Result<AuthenticationResponse>> Handle(
            RefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            return await _authenticationService.RefreshTokenAsync(request, cancellationToken);
        }
    }
}
