namespace Restaurant.Seeding.DataRecords.Personnel
{
    internal class PositionRecord
    {
        public Guid PublicId { get; set; }
        public Guid DepartmentPublicId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string PositionCode { get; set; } = null!;
    }
}
