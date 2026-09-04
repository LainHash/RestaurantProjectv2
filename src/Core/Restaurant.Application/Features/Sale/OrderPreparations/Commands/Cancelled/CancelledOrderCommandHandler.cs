using MediatR;
using Restaurant.Application.Services.Sale;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Sale.OrderPreparations.Commands.Cancelled
{
    internal class CancelledOrderCommandHandler(IOrderPreparationService orderPreparationService)
        : IRequestHandler<CancelledOrderCommand, Result>
    {
        private readonly IOrderPreparationService _orderPreparationService = orderPreparationService;

        public async Task<Result> Handle(CancelledOrderCommand request, CancellationToken cancellationToken)
        {
            var specification = new CancelledOrderSpecification(request);
            var response = await _orderPreparationService.CancelledOrderAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
