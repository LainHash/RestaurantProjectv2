using System.Text.Json.Serialization;

namespace Restaurant.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OtpPurpose
    {
        EmailVerification,
        PasswordReset,
        ChangeEmail,
        Login
    }
}
