using MediatR;
using Restaurant.Application.Services.Sale;
using Restaurant.Contract.DTOs.Sale.Orders;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Sale.Orders.Queries.GetById
{
    internal class GetOrderByIdQueryHandler(IOrderService orderService)
                : IRequestHandler<GetOrderByIdQuery, Result<OrderResponse>>
    {
        private readonly IOrderService _orderService = orderService;

        public async Task<Result<OrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetOrderByIdSpecification(request);
            var response = await _orderService.GetByIdAsync(specification, cancellationToken);
            return response;
        }
    }
}
