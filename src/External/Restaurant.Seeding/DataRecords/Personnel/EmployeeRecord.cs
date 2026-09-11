namespace Restaurant.Seeding.DataRecords.Personnel
{
    internal class EmployeeRecord
    {
        public Guid PublicId { get; set; }
        public Guid UserPublicId { get; set; }
        public Guid PositionPublicId { get; set; }
        public Guid BranchPublicId { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string Status { get; set; } = null!;
    }
}
