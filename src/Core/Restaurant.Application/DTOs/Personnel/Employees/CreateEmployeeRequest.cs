namespace Restaurant.Application.DTOs.Personnel.Employees
{
    public class CreateEmployeeRequest
    {
        public Guid RoleId { get; set; }

        public string? AvatarUrl { get; set; }

        public Guid UserId { get; set; }

        public Guid PositionId { get; set; }
        public Guid BranchId { get; set; }

        public DateTime HireDate { get; set; }
    }
}
