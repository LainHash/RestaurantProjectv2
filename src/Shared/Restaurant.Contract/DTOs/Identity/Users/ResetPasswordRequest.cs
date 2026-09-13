namespace Restaurant.Contract.DTOs.Identity.Users
{
    public class ResetPasswordRequest
    {
        public string NewPassword { get; set; } = null!;
        public string ConfirmNewPassword { get; set; } = null!;
        public string CurrentPassword { get; set; } = null!;
    }
}
