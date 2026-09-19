using Microsoft.Extensions.Options;
using Restaurant.Application.Services.Billing;
using Restaurant.Contract.Settings.Payment;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Restaurant.Infrastructure.Services.Billing
{
    internal class VnPayService(IOptions<VnPaySettings> options) : IVnPayService
    {
        private readonly VnPaySettings _settings = options.Value;

        public string CreatePaymentUrl(
            string transactionCode,
            decimal amount,
            string orderInfo,
            string clientIp,
            string? bankCode = null)
        {
            var nowGmt7 = DateTime.UtcNow.AddHours(7);
            var createDate = nowGmt7.ToString("yyyyMMddHHmmss");
            var expireDate = nowGmt7.AddMinutes(15).ToString("yyyyMMddHHmmss");

            // Amount multiplied by 100 to eliminate decimal places as per VNPAY spec
            var vnpAmount = ((long)(amount * 100)).ToString();

            var requestParams = new SortedList<string, string>(new VnPayCompare())
            {
                { "vnp_Version", _settings.Version },
                { "vnp_Command", _settings.Command },
                { "vnp_TmnCode", _settings.TmnCode },
                { "vnp_Amount", vnpAmount },
                { "vnp_CreateDate", createDate },
                { "vnp_CurrCode", _settings.CurrCode },
                { "vnp_IpAddr", string.IsNullOrWhiteSpace(clientIp) ? "127.0.0.1" : clientIp },
                { "vnp_Locale", _settings.Locale },
                { "vnp_OrderInfo", orderInfo },
                { "vnp_OrderType", "other" },
                { "vnp_ReturnUrl", _settings.ReturnUrl },
                { "vnp_TxnRef", transactionCode },
                { "vnp_ExpireDate", expireDate }
            };

            if (!string.IsNullOrWhiteSpace(bankCode))
            {
                requestParams.Add("vnp_BankCode", bankCode);
            }

            var queryBuilder = new StringBuilder();
            foreach (var (key, value) in requestParams)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    queryBuilder.Append(WebUtility.UrlEncode(key))
                                .Append('=')
                                .Append(WebUtility.UrlEncode(value))
                                .Append('&');
                }
            }

            var queryString = queryBuilder.ToString();
            var signData = queryString.Length > 0 ? queryString[..^1] : string.Empty;

            var secureHash = HmacSha512(_settings.HashSecret, signData);

            return $"{_settings.BaseUrl}?{queryString}vnp_SecureHash={secureHash}";
        }

        public bool ValidateSignature(IDictionary<string, string> responseParams, string inputHash)
        {
            var filtered = new SortedList<string, string>(new VnPayCompare());
            foreach (var (key, value) in responseParams)
            {
                if (!string.IsNullOrEmpty(key) &&
                    key.StartsWith("vnp_", StringComparison.OrdinalIgnoreCase) &&
                    !key.Equals("vnp_SecureHash", StringComparison.OrdinalIgnoreCase) &&
                    !key.Equals("vnp_SecureHashType", StringComparison.OrdinalIgnoreCase) &&
                    !string.IsNullOrEmpty(value))
                {
                    filtered.Add(key, value);
                }
            }

            var data = new StringBuilder();
            foreach (var (key, value) in filtered)
            {
                data.Append(WebUtility.UrlEncode(key))
                    .Append('=')
                    .Append(WebUtility.UrlEncode(value))
                    .Append('&');
            }

            var rawData = data.Length > 0 ? data.ToString()[..^1] : string.Empty;
            var myHash = HmacSha512(_settings.HashSecret, rawData);

            return myHash.Equals(inputHash, StringComparison.OrdinalIgnoreCase);
        }

        private static string HmacSha512(string key, string inputData)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var inputBytes = Encoding.UTF8.GetBytes(inputData);
            using var hmac = new HMACSHA512(keyBytes);
            var hashValue = hmac.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            foreach (var b in hashValue)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        private sealed class VnPayCompare : IComparer<string>
        {
            public int Compare(string? x, string? y)
            {
                if (x == y) return 0;
                if (x == null) return -1;
                if (y == null) return 1;
                return CompareInfo.GetCompareInfo("en-US").Compare(x, y, CompareOptions.Ordinal);
            }
        }
    }
}
