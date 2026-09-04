using AutoMapper;
using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Preparing;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Sale;
using Restaurant.Domain.Entities.Sale;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Sale;
using System.Net;

namespace Restaurant.Infrastructure.Services.Sale
{
    internal class OrderPreparationService : IOrderPreparationService
    {
        private readonly IOrderPreparationRepository _orderPreparationRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrderPreparationService(
            IOrderPreparationRepository orderPreparationRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _orderPreparationRepository = orderPreparationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> PreparingOrderAsync(
            PreparingOrderCommand command,
            PreparingOrderSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var orderPreparation = await _orderPreparationRepository.FindAsync(specification, cancellationToken);
            if (orderPreparation == null) 
            {
                return Result
                    .Fail(Error<OrderPreparation>.NotFound, HttpStatusCode.NotFound);
            }

            var order = orderPreparation.OrderDetail.Order;
            if (order.Status != OrderStatus.Confirmed)
            {
                return Result
                    .Fail("Order must be confirmed before preparation can start.", HttpStatusCode.Conflict);
            }

            order.Preparing();
            orderPreparation.Preparing();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed("Preparation started successfully.");
        }
    }
}
