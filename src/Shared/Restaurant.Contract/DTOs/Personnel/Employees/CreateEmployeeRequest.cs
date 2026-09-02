using Restaurant.Application.DTOs.Identity.PersonalProfiles;

namespace Restaurant.Application.DTOs.Personnel.Employees
{
    public class CreateEmployeeRequest
    {
        public Guid UserId { get; set; }

        public Guid PositionId { get; set; }
        public Guid BranchId { get; set; }

        public DateTime HireDate { get; set; }

        public CreatePersonalProfileRequest CreatePersonalProfileRequest { get; set; } = null!;
    }
}
