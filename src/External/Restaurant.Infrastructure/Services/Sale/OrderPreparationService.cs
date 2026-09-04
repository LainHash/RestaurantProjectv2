using AutoMapper;
using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Cancelled;
using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Preparing;
using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Ready;
using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Served;
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
            if (order.Status != OrderStatus.Confirmed && order.Status != OrderStatus.Preparing)
            {
                return Result
                    .Fail("Order must be confirmed before preparation can start.", HttpStatusCode.Conflict);
            }

            if (orderPreparation.Status != PreparationStatus.Pending)
            {
                return Result
                    .Fail("Order preparation must be in Pending status before preparation can start.", HttpStatusCode.Conflict);
            }

            if (order.Status == OrderStatus.Confirmed)
            {
                order.Preparing();
            }
            orderPreparation.Preparing();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed("Preparation started successfully.");
        }

        public async Task<Result> ReadyOrderAsync(
            ReadyOrderCommand command,
            ReadyOrderSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var orderPreparation = await _orderPreparationRepository.FindAsync(specification, cancellationToken);
            if (orderPreparation == null)
            {
                return Result
                    .Fail(Error<OrderPreparation>.NotFound, HttpStatusCode.NotFound);
            }

            var order = orderPreparation.OrderDetail.Order;
            if (order.Status != OrderStatus.Preparing)
            {
                return Result
                    .Fail("Order must be in Preparing status.", HttpStatusCode.Conflict);
            }

            if (orderPreparation.Status != PreparationStatus.Preparing)
            {
                return Result
                    .Fail("Order preparation must be in Preparing status before it can be marked as Ready.", HttpStatusCode.Conflict);
            }

            orderPreparation.Ready();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed("Order preparation marked as ready successfully.");
        }

        public async Task<Result> ServedOrderAsync(
            ServedOrderCommand command,
            ServedOrderSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var orderPreparation = await _orderPreparationRepository.FindAsync(specification, cancellationToken);
            if (orderPreparation == null)
            {
                return Result
                    .Fail(Error<OrderPreparation>.NotFound, HttpStatusCode.NotFound);
            }

            var order = orderPreparation.OrderDetail.Order;
            if (order.Status != OrderStatus.Preparing && order.Status != OrderStatus.Ready)
            {
                return Result
                    .Fail("Order must be in Preparing status.", HttpStatusCode.Conflict);
            }

            if (orderPreparation.Status != PreparationStatus.Ready)
            {
                return Result
                    .Fail("Order preparation must be in Ready status before it can be marked as Served.", HttpStatusCode.Conflict);
            }

            orderPreparation.Served();

            var allPreparations = order.OrderDetails.Select(od => od.OrderPreparation).ToList();
            if (allPreparations.All(p => p.Status == PreparationStatus.Served || p.Status == PreparationStatus.Cancelled))
            {
                order.Ready();
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed("Order preparation marked as served successfully.");
        }

        public async Task<Result> CancelledOrderAsync(
            CancelledOrderCommand command,
            CancelledOrderSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var orderPreparation = await _orderPreparationRepository.FindAsync(specification, cancellationToken);
            if (orderPreparation == null)
            {
                return Result
                    .Fail(Error<OrderPreparation>.NotFound, HttpStatusCode.NotFound);
            }

            if (orderPreparation.Status == PreparationStatus.Served)
            {
                return Result
                    .Fail("Cannot cancel an order preparation that has already been served.", HttpStatusCode.Conflict);
            }

            if (orderPreparation.Status == PreparationStatus.Cancelled)
            {
                return Result
                    .Fail("Order preparation is already cancelled.", HttpStatusCode.Conflict);
            }

            orderPreparation.Cancelled();

            var order = orderPreparation.OrderDetail.Order;
            var allPreparations = order.OrderDetails.Select(od => od.OrderPreparation).ToList();

            if (allPreparations.All(p => p.Status == PreparationStatus.Cancelled))
            {
                order.Cancelled();
            }
            else if (allPreparations.All(p => p.Status == PreparationStatus.Served || p.Status == PreparationStatus.Cancelled))
            {
                order.Ready();
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed("Order preparation cancelled successfully.");
        }
    }
}
