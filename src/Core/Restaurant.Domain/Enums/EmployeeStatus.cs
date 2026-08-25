using System.Text.Json.Serialization;

namespace Restaurant.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EmployeeStatus
    {
        Active,
        OnLeave,
        Suspended,
        Terminated
    }
}
