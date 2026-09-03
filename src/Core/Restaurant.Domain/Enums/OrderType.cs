using System.Text.Json.Serialization;

namespace Restaurant.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderType
    {
        DineIn,
        TakeAway,
        Delivery
    }
}
