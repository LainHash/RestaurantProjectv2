using AutoMapper;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Sale;
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
    }
}
