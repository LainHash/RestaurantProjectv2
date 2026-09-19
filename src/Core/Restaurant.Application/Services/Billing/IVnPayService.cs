namespace Restaurant.Application.Services.Billing
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(
            string transactionCode,
            decimal amount,
            string orderInfo,
            string clientIp,
            string? bankCode = null);

        bool ValidateSignature(
            IDictionary<string, string> responseParams,
            string inputHash);
    }
}
