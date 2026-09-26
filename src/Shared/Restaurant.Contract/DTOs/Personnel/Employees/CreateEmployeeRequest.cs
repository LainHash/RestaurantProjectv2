using Restaurant.Application.DTOs.Identity.PersonalProfiles;

namespace Restaurant.Application.DTOs.Personnel.Employees
{
    public class CreateEmployeeRequest
    {
        public Guid UserPublicId { get; set; }

        public Guid PositionPublicId { get; set; }
        public Guid BranchPublicId { get; set; }

        public DateTime HireDate { get; set; }

        public CreatePersonalProfileRequest CreatePersonalProfileRequest { get; set; } = null!;
    }
}
