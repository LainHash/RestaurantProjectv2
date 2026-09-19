using MediatR;
using Restaurant.Application.Features.Sale.Orders.Commands.Create;
using Restaurant.Application.Services.Sale;
using Restaurant.Contract.DTOs.Sale.Orders;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Sale.Orders.Commands.AddItems
{
    internal class AddOrderItemsCommandHandler(IOrderService orderService)
        : IRequestHandler<AddOrderItemsCommand, Result<OrderResponse>>
    {
        private readonly IOrderService _orderService = orderService;

        public async Task<Result<OrderResponse>> Handle(AddOrderItemsCommand request, CancellationToken cancellationToken)
        {
            var specification = new CreateOrderSpecification();
            var response = await _orderService.AddItemsAsync(request.OrderId, request.Body, specification, cancellationToken);
            return response;
        }
    }
}
