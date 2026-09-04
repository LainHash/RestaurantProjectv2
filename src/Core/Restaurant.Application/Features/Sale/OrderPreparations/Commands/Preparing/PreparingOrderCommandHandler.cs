using MediatR;
using Restaurant.Application.Services.Sale;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Sale.OrderPreparations.Commands.Preparing
{
    internal class PreparingOrderCommandHandler(IOrderPreparationService orderPreparationService)
                : IRequestHandler<PreparingOrderCommand, Result>
    {
        private readonly IOrderPreparationService _orderPreparationService = orderPreparationService;

        public async Task<Result> Handle(PreparingOrderCommand request, CancellationToken cancellationToken)
        {
            var specification = new PreparingOrderSpecification(request);
            var response = await _orderPreparationService.PreparingOrderAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
