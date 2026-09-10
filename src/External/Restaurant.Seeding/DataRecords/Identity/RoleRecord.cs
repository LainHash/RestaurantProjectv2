namespace Restaurant.Seeding.DataRecords.Identity
{
    internal class RoleRecord
    {
        public Guid PublicId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
