using Restaurant.Application.DTOs.Pricing.Discounts;
using Restaurant.Application.Features.Pricing.Discounts.Queries.GetAll;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Pricing
{
    public interface IDiscountService
    {
        Task<PageResult<IEnumerable<DiscountResponse>>> GetAllAsync(
            GetAllDiscountsSpecification specification,
            CancellationToken cancellationToken = default);
    }
}
