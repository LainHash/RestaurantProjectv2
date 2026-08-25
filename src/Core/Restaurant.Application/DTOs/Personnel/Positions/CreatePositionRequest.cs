namespace Restaurant.Contract.DTOs.Personnel.Positions
{
    public class CreatePositionRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
