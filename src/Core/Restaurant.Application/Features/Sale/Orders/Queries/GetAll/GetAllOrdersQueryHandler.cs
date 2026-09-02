using MediatR;
using Restaurant.Application.Services.Sale;
using Restaurant.Contract.DTOs.Sale.Orders;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Sale.Orders.Queries.GetAll
{
    internal class GetAllOrdersQueryHandler(IOrderService orderService)
                : IRequestHandler<GetAllOrdersQuery, PageResult<IEnumerable<OrderResponse>>>
    {
        private readonly IOrderService _orderService = orderService;

        public async Task<PageResult<IEnumerable<OrderResponse>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllOrdersSpecification(request);
            var response = await _orderService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}
