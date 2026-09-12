using Restaurant.Domain.Enums;

namespace Restaurant.Contract.DTOs.Auth
{
    public class ResendVerificationRequest
    {
        public string Email { get; set; } = string.Empty;
        public OtpPurpose Purpose { get; set; }
    }
}
