using MediatR;
using Restaurant.Application.Services.Sale;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Sale.OrderPreparations.Commands.Serve
{
    internal class ServeOrderCommandHandler(IOrderPreparationService orderPreparationService)
        : IRequestHandler<ServeOrderCommand, Result>
    {
        private readonly IOrderPreparationService _orderPreparationService = orderPreparationService;

        public async Task<Result> Handle(ServeOrderCommand request, CancellationToken cancellationToken)
        {
            var specification = new ServeOrderSpecification(request);
            var response = await _orderPreparationService.ServeOrderAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
