using System.Text.Json.Serialization;

namespace Restaurant.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PreparationStatus
    {
        Pending,
        Preparing,
        Ready,
        Served,
        Cancelled
    }
}
