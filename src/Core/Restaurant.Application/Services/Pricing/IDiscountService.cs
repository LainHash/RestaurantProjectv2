using Restaurant.Application.DTOs.Pricing.Discounts;
using Restaurant.Application.Features.Pricing.Discounts.Commands.Create;
using Restaurant.Application.Features.Pricing.Discounts.Commands.Update;
using Restaurant.Application.Features.Pricing.Discounts.Queries.GetAll;
using Restaurant.Domain.Entities.Pricing;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Services.Pricing
{
    public interface IDiscountService
    {
        Task<PageResult<IEnumerable<DiscountResponse>>> GetAllAsync(
            GetAllDiscountsSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<DiscountResponse>> CreateAsync(
            CreateDiscountCommand command,
            CancellationToken cancellationToken = default);

        Task<Result<DiscountResponse>> UpdateAsync(
            UpdateDiscountCommand command,
            UpdateDiscountSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result> DeleteAsync(
            ISpecification<Discount> specification,
            CancellationToken cancellationToken = default);

        Task<Result> RestoreAsync(
            ISpecification<Discount> specification,
            CancellationToken cancellationToken = default);
    }
}
