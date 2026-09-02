using AutoMapper;
using Restaurant.Application.Features.Sale.Orders.Queries.GetAll;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Sale;
using Restaurant.Contract.DTOs.Sale.Orders;
using Restaurant.Domain.Entities.Sale;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Sale;

namespace Restaurant.Infrastructure.Services.Sale
{
    internal class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(
            IOrderRepository orderRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PageResult<IEnumerable<OrderResponse>>> GetAllAsync(
            GetAllOrdersSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var totalItems = await _orderRepository.CountAsync(specification, cancellationToken);

            var orders = await _orderRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<OrderResponse>>(orders);
            return PageResult<IEnumerable<OrderResponse>>
                .Succeed(response, Success<Order>.Retrieved, totalItems, specification.Skip, specification.Take);
        }

    }
}
