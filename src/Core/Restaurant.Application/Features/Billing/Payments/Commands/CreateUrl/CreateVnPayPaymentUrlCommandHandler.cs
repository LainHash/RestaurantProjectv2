using MediatR;
using NanoidDotNet;
using Restaurant.Application.Services.Billing;
using Restaurant.Application.Services.Business;
using Restaurant.Contract.DTOs.Billing.Payments;
using Restaurant.Domain.Entities.Billing;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Billing;
using System.Net;

namespace Restaurant.Application.Features.Billing.Payments.Commands.CreateUrl
{
    public class CreateVnPayPaymentUrlCommandHandler(
        IInvoiceRepository invoiceRepository,
        IPaymentRepository paymentRepository,
        IVnPayService vnPayService,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateVnPayPaymentUrlCommand, Result<VnPayPaymentUrlResponse>>
    {
        private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;
        private readonly IPaymentRepository _paymentRepository = paymentRepository;
        private readonly IVnPayService _vnPayService = vnPayService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<VnPayPaymentUrlResponse>> Handle(
            CreateVnPayPaymentUrlCommand command,
            CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepository.FindByPublicIdAsync(command.Body.InvoiceId, cancellationToken);
            if (invoice == null)
            {
                return Result<VnPayPaymentUrlResponse>.Fail(
                    Error.NotFound("Invoice"),
                    HttpStatusCode.NotFound);
            }

            if (invoice.Status == InvoiceStatus.Paid)
            {
                return Result<VnPayPaymentUrlResponse>.Fail(
                    "Invoice has already been paid.",
                    HttpStatusCode.Conflict);
            }

            if (invoice.Status == InvoiceStatus.Cancelled)
            {
                return Result<VnPayPaymentUrlResponse>.Fail(
                    "Cannot pay for a cancelled invoice.",
                    HttpStatusCode.Conflict);
            }

            if (invoice.TotalAmount <= 0)
            {
                return Result<VnPayPaymentUrlResponse>.Fail(
                    "Invoice total amount must be greater than zero.",
                    HttpStatusCode.BadRequest);
            }

            // Create pending payment
            var payment = new Payment(invoice.Id, invoice.TotalAmount, PaymentMethod.VNPay);
            _paymentRepository.Add(payment);

            // Generate unique transaction code for VNPay (vnp_TxnRef)
            var nowGmt7 = DateTime.UtcNow.AddHours(7);
            var transactionCode = $"{nowGmt7:yyyyMMddHHmmss}_{Nanoid.Generate("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", 8)}";

            // Attach transaction
            var transaction = new PaymentTransaction(
                payment.Id,
                "VNPay",
                transactionCode,
                invoice.TotalAmount,
                command.Body.BankCode);
            payment.AddTransaction(transaction);

            // Build VNPay URL
            var orderInfo = $"Thanh toan hoa don {invoice.InvoiceCode}";
            var paymentUrl = _vnPayService.CreatePaymentUrl(
                transactionCode,
                invoice.TotalAmount,
                orderInfo,
                command.ClientIp,
                command.Body.BankCode);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = new VnPayPaymentUrlResponse
            {
                PaymentUrl = paymentUrl,
                TransactionCode = transactionCode,
                PaymentPublicId = payment.PublicId,
                Amount = invoice.TotalAmount
            };

            return Result<VnPayPaymentUrlResponse>.Succeed(
                response,
                "VNPay payment URL generated successfully.");
        }
    }
}
