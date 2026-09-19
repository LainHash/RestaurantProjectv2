using MediatR;
using Restaurant.Contract.DTOs.Billing.Payments;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Billing.Payments.Commands.CreateUrl
{
    public record CreateVnPayPaymentUrlCommand(
        CreateVnPayPaymentUrlRequest Body,
        string ClientIp) : IRequest<Result<VnPayPaymentUrlResponse>>;
}
