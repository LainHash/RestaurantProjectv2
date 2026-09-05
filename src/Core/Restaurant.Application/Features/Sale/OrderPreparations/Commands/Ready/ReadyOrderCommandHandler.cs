using MediatR;
using Restaurant.Application.Services.Sale;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Sale.OrderPreparations.Commands.Ready
{
    internal class ReadyOrderCommandHandler(IOrderPreparationService orderPreparationService)
        : IRequestHandler<ReadyOrderCommand, Result>
    {
        private readonly IOrderPreparationService _orderPreparationService = orderPreparationService;

        public async Task<Result> Handle(ReadyOrderCommand request, CancellationToken cancellationToken)
        {
            var specification = new ReadyOrderSpecification(request);
            var response = await _orderPreparationService.ReadyOrderAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
