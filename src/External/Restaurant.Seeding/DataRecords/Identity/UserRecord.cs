namespace Restaurant.Seeding.DataRecords.Identity
{
    internal class UserRecord
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public bool IsActive { get; set; }
        public string RoleName { get; set; } = null!;
    }
}
