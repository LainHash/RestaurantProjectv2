using MediatR;
using Restaurant.Application.Services.Billing;
using Restaurant.Contract.DTOs.Billing.Payments;
using Restaurant.Domain.Models.Results;
using System.Globalization;
using System.Net;

namespace Restaurant.Application.Features.Billing.Payments.Queries.ProcessReturn
{
    public class ProcessVnPayReturnQueryHandler(
        IVnPayService vnPayService) : IRequestHandler<ProcessVnPayReturnQuery, Result<VnPayReturnResponse>>
    {
        private readonly IVnPayService _vnPayService = vnPayService;

        public Task<Result<VnPayReturnResponse>> Handle(
            ProcessVnPayReturnQuery request,
            CancellationToken cancellationToken)
        {
            var query = request.QueryParams;
            if (query.Count == 0 || !query.TryGetValue("vnp_SecureHash", out var inputHash) || string.IsNullOrEmpty(inputHash))
            {
                return Task.FromResult(Result<VnPayReturnResponse>.Fail(
                    "Missing VNPay return data or signature.",
                    HttpStatusCode.BadRequest));
            }

            if (!_vnPayService.ValidateSignature(query, inputHash))
            {
                return Task.FromResult(Result<VnPayReturnResponse>.Fail(
                    "Invalid signature.",
                    HttpStatusCode.BadRequest));
            }

            query.TryGetValue("vnp_TxnRef", out var txnRef);
            txnRef ??= string.Empty;

            query.TryGetValue("vnp_ResponseCode", out var responseCode);
            responseCode ??= string.Empty;

            query.TryGetValue("vnp_TransactionStatus", out var transactionStatus);
            transactionStatus ??= string.Empty;

            query.TryGetValue("vnp_BankCode", out var bankCode);
            query.TryGetValue("vnp_TransactionNo", out var vnpayTranNo);

            decimal amount = 0m;
            if (query.TryGetValue("vnp_Amount", out var rawAmountStr) && decimal.TryParse(rawAmountStr, out var rawAmount))
            {
                amount = rawAmount / 100m;
            }

            DateTime? payDate = null;
            if (query.TryGetValue("vnp_PayDate", out var payDateStr) &&
                DateTime.TryParseExact(payDateStr, "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                payDate = parsedDate;
            }

            var isSuccess = responseCode == "00" && transactionStatus == "00";
            var message = GetResponseMessage(responseCode, isSuccess);

            var response = new VnPayReturnResponse
            {
                IsSuccess = isSuccess,
                Message = message,
                TransactionCode = txnRef,
                VnPayTransactionNo = vnpayTranNo,
                Amount = amount,
                BankCode = bankCode,
                ResponseCode = responseCode,
                PayDate = payDate
            };

            return Task.FromResult(Result<VnPayReturnResponse>.Succeed(response, message));
        }

        private static string GetResponseMessage(string responseCode, bool isSuccess)
        {
            if (isSuccess)
            {
                return "Giao dịch thanh toán VNPAY thành công.";
            }

            return responseCode switch
            {
                "07" => "Trừ tiền thành công. Giao dịch bị nghi ngờ gian lận.",
                "09" => "Thẻ/Tài khoản chưa đăng ký dịch vụ Internet Banking.",
                "10" => "Xác thực thông tin thẻ/tài khoản không đúng quá 3 lần.",
                "11" => "Đã hết hạn chờ thanh toán.",
                "12" => "Thẻ/Tài khoản của khách hàng đang bị khóa.",
                "13" => "Quý khách nhập sai mật khẩu OTP xác thực giao dịch.",
                "24" => "Khách hàng đã hủy giao dịch.",
                "51" => "Tài khoản không đủ số dư để thực hiện giao dịch.",
                "65" => "Tài khoản đã vượt quá hạn mức giao dịch trong ngày.",
                "75" => "Ngân hàng thanh toán đang bảo trì.",
                "79" => "Nhập sai mật khẩu thanh toán quá số lần quy định.",
                _ => $"Giao dịch không thành công. Mã lỗi: {responseCode}"
            };
        }
    }
}
