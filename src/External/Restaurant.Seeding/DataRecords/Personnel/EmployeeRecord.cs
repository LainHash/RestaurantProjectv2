namespace Restaurant.Seeding.DataRecords.Personnel
{
    internal class EmployeeRecord
    {
        public string UserName { get; set; } = null!;
        public string PositionName { get; set; } = null!;
        public string BranchCode { get; set; } = null!;
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string Status { get; set; } = null!;
    }
}
