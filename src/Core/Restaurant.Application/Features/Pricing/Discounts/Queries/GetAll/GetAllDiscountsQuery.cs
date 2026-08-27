using MediatR;
using Restaurant.Application.DTOs.Pricing.Discounts;
using Restaurant.Domain.Models;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Pricing.Discounts.Queries.GetAll
{
    public record GetAllDiscountsQuery()
        : PageQuery, IRequest<PageResult<IEnumerable<DiscountResponse>>>
    {
    }
}
