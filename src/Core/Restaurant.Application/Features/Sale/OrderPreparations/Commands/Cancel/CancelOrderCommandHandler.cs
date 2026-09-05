using MediatR;
using Restaurant.Application.Services.Sale;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Sale.OrderPreparations.Commands.Cancel
{
    internal class CancelOrderCommandHandler(IOrderPreparationService orderPreparationService)
        : IRequestHandler<CancelOrderCommand, Result>
    {
        private readonly IOrderPreparationService _orderPreparationService = orderPreparationService;

        public async Task<Result> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var specification = new CancelOrderSpecification(request);
            var response = await _orderPreparationService.CancelOrderAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
