using MediatR;
using Restaurant.Application.Services.Sale;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Sale.OrderPreparations.Commands.Served
{
    internal class ServedOrderCommandHandler(IOrderPreparationService orderPreparationService)
        : IRequestHandler<ServedOrderCommand, Result>
    {
        private readonly IOrderPreparationService _orderPreparationService = orderPreparationService;

        public async Task<Result> Handle(ServedOrderCommand request, CancellationToken cancellationToken)
        {
            var specification = new ServedOrderSpecification(request);
            var response = await _orderPreparationService.ServedOrderAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
