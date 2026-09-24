using MediatR;
using Restaurant.Application.Services.Billing;
using Restaurant.Application.Services.Business;
using Restaurant.Contract.DTOs.Billing.Payments;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Repositories.Billing;
using Restaurant.Domain.Repositories.Territory;
using System.Text.Json;

namespace Restaurant.Application.Features.Billing.Payments.Commands.HandleIpn
{
    public class HandleVnPayIpnCommandHandler(
        IPaymentTransactionRepository paymentTransactionRepository,
        IRestaurantTableRepository restaurantTableRepository,
        IVnPayService vnPayService,
        IUnitOfWork unitOfWork) : IRequestHandler<HandleVnPayIpnCommand, VnPayIpnResponse>
    {
        private readonly IPaymentTransactionRepository _paymentTransactionRepository = paymentTransactionRepository;
        private readonly IRestaurantTableRepository _restaurantTableRepository = restaurantTableRepository;
        private readonly IVnPayService _vnPayService = vnPayService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<VnPayIpnResponse> Handle(
            HandleVnPayIpnCommand command,
            CancellationToken cancellationToken)
        {
            var query = command.QueryParams;
            if (query.Count == 0 || !query.TryGetValue("vnp_SecureHash", out var inputHash) || string.IsNullOrEmpty(inputHash))
            {
                return VnPayIpnResponse.InputDataRequired();
            }

            // 1. Verify Checksum
            if (!_vnPayService.ValidateSignature(query, inputHash))
            {
                return VnPayIpnResponse.InvalidSignature();
            }

            // 2. Extract TxnRef and lookup transaction
            if (!query.TryGetValue("vnp_TxnRef", out var txnRef) || string.IsNullOrEmpty(txnRef))
            {
                return VnPayIpnResponse.OrderNotFound();
            }

            var transaction = await _paymentTransactionRepository
                .FindByTransactionCodeWithPaymentAsync(txnRef, cancellationToken);

            if (transaction == null)
            {
                return VnPayIpnResponse.OrderNotFound();
            }

            // 3. Verify Amount
            if (!query.TryGetValue("vnp_Amount", out var rawAmountStr) ||
                !decimal.TryParse(rawAmountStr, out var rawAmount) ||
                (rawAmount / 100m) != transaction.Amount)
            {
                return VnPayIpnResponse.InvalidAmount();
            }

            // 4. Idempotency Check: if already processed, return 02
            if (transaction.Status != PaymentTransactionStatus.Pending)
            {
                return VnPayIpnResponse.OrderAlreadyConfirmed();
            }

            // 5. Process Payment Status
            var responseCode = query.TryGetValue("vnp_ResponseCode", out var rc) ? rc : string.Empty;
            var transactionStatus = query.TryGetValue("vnp_TransactionStatus", out var ts) ? ts : string.Empty;
            var responseJson = JsonSerializer.Serialize(query);

            var isSuccess = responseCode == "00" && transactionStatus == "00";

            if (isSuccess)
            {
                transaction.Success(responseJson);
                transaction.Payment.MarkAsPaid();
                transaction.Payment.Invoice.MarkAsPaid();

                // Check and update Order status
                var order = transaction.Payment.Invoice.Order;
                if (order != null)
                {
                    var allPreparationsDone = order.OrderDetails.All(od =>
                        od.OrderPreparation == null ||
                        od.OrderPreparation.Status == PreparationStatus.Served ||
                        od.OrderPreparation.Status == PreparationStatus.Cancelled);

                    if (allPreparationsDone)
                    {
                        order.Complete();

                        // Release table if DineIn order
                        if (order.Type == OrderType.DineIn && order.RestaurantTableId.HasValue)
                        {
                            var table = await _restaurantTableRepository
                                .FindByIdAsync(order.RestaurantTableId.Value, cancellationToken);
                            table?.Release();
                        }
                    }
                    else
                    {
                        order.Confirm();
                    }
                }
            }
            else
            {
                transaction.Failed(responseJson);
                transaction.Payment.MarkAsFailed();
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return VnPayIpnResponse.Success();
        }
    }
}
