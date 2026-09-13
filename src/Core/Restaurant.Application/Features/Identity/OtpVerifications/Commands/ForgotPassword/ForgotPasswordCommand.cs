using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.OtpVerifications.Commands.ForgotPassword
{
    public record ForgotPasswordCommand(string Email)
        : IRequest<Result>
    {
    }
}
