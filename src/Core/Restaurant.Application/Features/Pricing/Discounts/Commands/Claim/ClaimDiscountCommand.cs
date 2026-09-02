using MediatR;
using Restaurant.Application.DTOs.Pricing.Discounts;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Pricing.Discounts.Commands.Claim
{
    public record ClaimDiscountCommand(Guid UserId, ClaimDiscountRequest Body)
        : IRequest<Result>
    {
    }
}
