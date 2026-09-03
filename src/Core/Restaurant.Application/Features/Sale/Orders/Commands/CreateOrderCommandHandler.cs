using MediatR;
using Restaurant.Application.Services.Sale;
using Restaurant.Contract.DTOs.Sale.Orders;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Sale.Orders.Commands
{
    internal class CreateOrderCommandHandler(IOrderService orderService)
                : IRequestHandler<CreateOrderCommand, Result<OrderResponse>>
    {
        private readonly IOrderService _orderService = orderService;

        public async Task<Result<OrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var specification = new CreateOrderSpecification();
            var response = await _orderService.CreateAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
