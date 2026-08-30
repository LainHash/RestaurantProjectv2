using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Pricing.Discounts.Commands.Delete
{
    public record DeleteDiscountCommand(Guid Id)
        : IRequest<Result>
    {
    }
}
