using MediatR;
using Restaurant.Contract.DTOs.Billing.Payments;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Billing.Payments.Queries.ProcessReturn
{
    public record ProcessVnPayReturnQuery(
        IDictionary<string, string> QueryParams) : IRequest<Result<VnPayReturnResponse>>;
}
