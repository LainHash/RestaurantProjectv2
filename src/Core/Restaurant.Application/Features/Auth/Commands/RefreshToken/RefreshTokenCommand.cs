using MediatR;
using Restaurant.Contract.DTOs.Auth;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Auth.Commands.RefreshToken
{
    public record RefreshTokenCommand(RefreshTokenRequest Body)
        : IRequest<Result<AuthenticationResponse>>;
}
