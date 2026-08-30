using MediatR;
using Restaurant.Application.DTOs.Pricing.Discounts;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Pricing.Discounts.Commands.Create
{
    public record CreateDiscountCommand(CreateDiscountRequest Body)
        : IRequest<Result<DiscountResponse>>
    {
    }
}
