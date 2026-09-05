using System.Text.Json.Serialization;

namespace Restaurant.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum InvoiceStatus
    {
        Unpaid,
        PartiallyPaid,
        Paid,
        Cancelled,
        Refunded,
        PartiallyRefunded
    }
}
