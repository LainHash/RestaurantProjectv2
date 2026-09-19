using MediatR;
using Restaurant.Contract.DTOs.Billing.Payments;

namespace Restaurant.Application.Features.Billing.Payments.Commands.HandleIpn
{
    public record HandleVnPayIpnCommand(
        IDictionary<string, string> QueryParams) : IRequest<VnPayIpnResponse>;
}
