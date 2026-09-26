using System.Text.Json.Serialization;

namespace Restaurant.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ReservationStatus
    {
        Pending,
        Confirmed,
        Seated,
        Completed,
        Cancelled,
        NoShow
    }
}
