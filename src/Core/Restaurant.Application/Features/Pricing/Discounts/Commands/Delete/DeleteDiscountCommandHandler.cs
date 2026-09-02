using MediatR;
using Restaurant.Application.Services.Pricing;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Pricing.Discounts.Commands.Delete
{
    internal class DeleteDiscountCommandHandler(IDiscountService discountService)
                : IRequestHandler<DeleteDiscountCommand, Result>
    {
        private readonly IDiscountService _discountService = discountService;

        public async Task<Result> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            var specification = new DeleteDiscountSpecification(request);
            var response = await _discountService.DeleteAsync(specification, cancellationToken);
            return response;
        }
    }
}
