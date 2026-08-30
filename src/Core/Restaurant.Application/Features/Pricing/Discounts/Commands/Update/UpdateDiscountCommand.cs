using MediatR;
using Restaurant.Application.DTOs.Pricing.Discounts;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Pricing.Discounts.Commands.Update
{
    public record UpdateDiscountCommand(Guid Id, UpdateDiscountRequest Body)
        : IRequest<Result<DiscountResponse>>
    {
    }
}
