using System.Text.Json.Serialization;

namespace Restaurant.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DiscountCustomerStatus
    {
        Available = 1,
        Used = 2,
        Expired = 3,
        Revoked = 4
    }
}
