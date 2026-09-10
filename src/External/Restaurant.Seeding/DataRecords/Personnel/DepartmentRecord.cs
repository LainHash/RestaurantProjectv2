namespace Restaurant.Seeding.DataRecords.Personnel
{
    internal class DepartmentRecord
    {
        public Guid PublicId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string DepartmentCode { get; set; } = null!;
    }
}
