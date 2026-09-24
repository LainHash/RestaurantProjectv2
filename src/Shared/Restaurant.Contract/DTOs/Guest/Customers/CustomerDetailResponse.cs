using Restaurant.Contract.DTOs.Guest.Wallets;
using Restaurant.Contract.DTOs.Identity.PersonalProfiles;
using Restaurant.Contract.DTOs.Identity.Users;

namespace Restaurant.Contract.DTOs.Guest.Customers
{
    public class CustomerDetailResponse
    {
        public Guid Id { get; set; }
        public string CustomerCode { get; set; } = string.Empty;

        public string? AvatarUrl { get; set; }

        public UserResponse User { get; set; } = null!;
        public PersonalProfileResponse PersonalProfile { get; set; } = null!;
        public WalletResponse Wallet { get; set; } = null!;
    }
}
