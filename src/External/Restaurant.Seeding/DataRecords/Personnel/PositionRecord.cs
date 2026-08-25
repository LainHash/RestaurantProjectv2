namespace Restaurant.Seeding.DataRecords.Personnel
{
    internal class PositionRecord
    {
        public string DepartmentCode { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string PositionCode { get; set; } = null!;
    }
}
