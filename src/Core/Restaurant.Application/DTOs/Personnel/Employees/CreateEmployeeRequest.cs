namespace Restaurant.Application.DTOs.Personnel.Employees
{
    public class CreateEmployeeRequest
    {
        public string RoleName { get; set; } = null!;

        public string? AvatarUrl { get; set; }

        public Guid PositionId { get; set; }

        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
    }
}
