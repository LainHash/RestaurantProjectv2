namespace Restaurant.Seeding.DataRecords.Personnel
{
    internal class EmployeeRecord
    {
        public Guid PublicId { get; set; }
        public Guid UserId { get; set; }
        public Guid PositionId { get; set; }
        public Guid BranchId { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string Status { get; set; } = null!;
    }
}
