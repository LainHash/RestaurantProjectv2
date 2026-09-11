namespace Restaurant.Seeding.DataRecords.Identity
{
    internal class UserRecord
    {
        public Guid PublicId { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public bool IsActive { get; set; }
        public Guid RolePublicId { get; set; }
    }
}
