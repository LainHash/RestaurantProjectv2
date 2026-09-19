using System.Text.Json.Serialization;

namespace Restaurant.Contract.DTOs.Billing.Payments
{
    public class VnPayIpnResponse
    {
        [JsonPropertyName("RspCode")]
        public string RspCode { get; set; } = string.Empty;

        [JsonPropertyName("Message")]
        public string Message { get; set; } = string.Empty;

        public VnPayIpnResponse() { }

        public VnPayIpnResponse(string rspCode, string message)
        {
            RspCode = rspCode;
            Message = message;
        }

        public static VnPayIpnResponse Success() => new("00", "Confirm Success");
        public static VnPayIpnResponse OrderNotFound() => new("01", "Order not found");
        public static VnPayIpnResponse OrderAlreadyConfirmed() => new("02", "Order already confirmed");
        public static VnPayIpnResponse InvalidAmount() => new("04", "invalid amount");
        public static VnPayIpnResponse InvalidSignature() => new("97", "Invalid signature");
        public static VnPayIpnResponse InputDataRequired() => new("99", "Input data required");
    }
}
