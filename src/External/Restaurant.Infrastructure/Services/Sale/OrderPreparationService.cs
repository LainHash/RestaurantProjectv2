using AutoMapper;
using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Cancel;
using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Prepare;
using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Ready;
using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Serve;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Sale;
using Restaurant.Domain.Entities.Sale;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Sale;
using Restaurant.Domain.Repositories.Territory;
using System.Net;

namespace Restaurant.Infrastructure.Services.Sale
{
    internal class OrderPreparationService : IOrderPreparationService
    {
        private readonly IOrderPreparationRepository _orderPreparationRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IRestaurantTableRepository _restaurantTableRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrderPreparationService(
            IOrderPreparationRepository orderPreparationRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IOrderDetailRepository orderDetailRepository,
            IOrderRepository orderRepository,
            IRestaurantTableRepository restaurantTableRepository)
        {
            _orderPreparationRepository = orderPreparationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _restaurantTableRepository = restaurantTableRepository;
        }

        public async Task<Result> PrepareOrderAsync(
            PrepareOrderCommand command,
            PrepareOrderSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var orderPreparation = await _orderPreparationRepository.FindAsync(specification, cancellationToken);
            if (orderPreparation == null)
            {
                return Result
                    .Fail(Error.NotFound("OrderPreparation"), HttpStatusCode.NotFound);
            }

            if(orderPreparation.Status == PreparationStatus.Cancelled)
            {
                return Result
                    .Fail("Cancelled order detail cannot be prepare.", HttpStatusCode.NotFound);
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
                .Succeed("Order preparation started successfully.");
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
                    .Fail(Error.NotFound("OrderPreparation"), HttpStatusCode.NotFound);
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

        public async Task<Result> ServeOrderAsync(
            ServeOrderCommand command,
            ServeOrderSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var orderPreparation = await _orderPreparationRepository.FindAsync(specification, cancellationToken);
            if (orderPreparation == null)
            {
                return Result
                    .Fail(Error.NotFound("OrderPreparation"), HttpStatusCode.NotFound);
            }

            var order = await _orderRepository.FindWithOrderDetailAsync(orderPreparation.OrderDetail.Order.Id, cancellationToken);
            if (order!.Status != OrderStatus.Preparing && order.Status != OrderStatus.Served)
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
                order.Served();

                // Release table when all items of a DineIn order are served
                if (order.Type == OrderType.DineIn && order.RestaurantTableId.HasValue)
                {
                    var table = await _restaurantTableRepository
                        .FindByIdAsync(order.RestaurantTableId.Value, cancellationToken);
                    table?.Release();
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed("Order preparation marked as served successfully.");
        }

        public async Task<Result> CancelOrderAsync(
            CancelOrderCommand command,
            CancelOrderSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var orderPreparation = await _orderPreparationRepository.FindAsync(specification, cancellationToken);
            if (orderPreparation == null)
            {
                return Result
                    .Fail(Error.NotFound("OrderPreparation"), HttpStatusCode.NotFound);
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

            var order = await _orderRepository.FindWithOrderDetailAsync(orderPreparation.OrderDetail.Order.Id, cancellationToken);
            var allPreparations = order!.OrderDetails.Select(od => od.OrderPreparation).ToList();

            if (allPreparations.All(p => p.Status == PreparationStatus.Cancelled))
            {
                order.Cancelled();

                // Release table when a DineIn order is fully cancelled
                if (order.Type == OrderType.DineIn && order.RestaurantTableId.HasValue)
                {
                    var table = await _restaurantTableRepository
                        .FindByIdAsync(order.RestaurantTableId.Value, cancellationToken);
                    table?.Release();
                }
            }
            else if (allPreparations.All(p => p.Status == PreparationStatus.Served || p.Status == PreparationStatus.Cancelled))
            {
                order.Served();

                // Release table when remaining items of a DineIn order are all done
                if (order.Type == OrderType.DineIn && order.RestaurantTableId.HasValue)
                {
                    var table = await _restaurantTableRepository
                        .FindByIdAsync(order.RestaurantTableId.Value, cancellationToken);
                    table?.Release();
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed("Order preparation cancelled successfully.");
        }
    }
}
