using System.Text.Json.Serialization;

namespace Restaurant.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentStatus
    {
        Pending,
        Processing,
        Paid,
        Failed,
        Cancelled,
        Refunded,
        PartiallyRefunded
    }
}
