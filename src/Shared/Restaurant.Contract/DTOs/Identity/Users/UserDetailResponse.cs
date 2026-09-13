using Restaurant.Contract.DTOs.Identity.PersonalProfiles;

namespace Restaurant.Contract.DTOs.Identity.Users
{
    public class UserDetailResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;

        public PersonalProfileResponse? PersonalProfile { get; set; } = null!;
    }
}
