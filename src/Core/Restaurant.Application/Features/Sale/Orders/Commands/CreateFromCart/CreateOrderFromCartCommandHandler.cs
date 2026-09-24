using MediatR;
using Restaurant.Application.Features.Sale.Orders.Commands.Create;
using Restaurant.Application.Services.Sale;
using Restaurant.Contract.DTOs.Sale.Orders;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Sale.Orders.Commands.CreateFromCart
{
    internal class CreateOrderFromCartCommandHandler(IOrderService orderService)
        : IRequestHandler<CreateOrderFromCartCommand, Result<OrderResponse>>
    {
        private readonly IOrderService _orderService = orderService;

        public async Task<Result<OrderResponse>> Handle(CreateOrderFromCartCommand request, CancellationToken cancellationToken)
        {
            var specification = new CreateOrderSpecification();
            var response = await _orderService.CreateFromCartAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
