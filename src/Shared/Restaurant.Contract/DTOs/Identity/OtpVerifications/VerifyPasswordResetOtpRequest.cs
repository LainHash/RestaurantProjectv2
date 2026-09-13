namespace Restaurant.Contract.DTOs.Identity.OtpVerifications
{
    public class VerifyPasswordResetOtpRequest
    {
        public string Code { get; set; } = string.Empty;
    }
}
