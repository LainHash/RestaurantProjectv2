using MediatR;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Pricing.Discounts.Commands.Restore
{
    public record RestoreDiscountCommand(Guid Id)
        : IRequest<Result>
    {
    }
}
