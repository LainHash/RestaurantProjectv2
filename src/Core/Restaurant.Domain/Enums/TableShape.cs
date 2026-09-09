using System.Text.Json.Serialization;

namespace Restaurant.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TableShape
    {
        Square,
        Rectangle,
        Round,
        Long,
        Other
    }
}
