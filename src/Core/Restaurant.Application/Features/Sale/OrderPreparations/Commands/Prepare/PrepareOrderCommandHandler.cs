using MediatR;
using Restaurant.Application.Services.Sale;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Sale.OrderPreparations.Commands.Prepare
{
    internal class PrepareOrderCommandHandler(IOrderPreparationService orderPreparationService)
                : IRequestHandler<PrepareOrderCommand, Result>
    {
        private readonly IOrderPreparationService _orderPreparationService = orderPreparationService;

        public async Task<Result> Handle(PrepareOrderCommand request, CancellationToken cancellationToken)
        {
            var specification = new PrepareOrderSpecification(request);
            var response = await _orderPreparationService.PrepareOrderAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
