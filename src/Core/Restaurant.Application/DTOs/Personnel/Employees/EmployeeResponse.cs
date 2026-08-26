using Restaurant.Contract.DTOs.Identity.PersonalProfiles;
using Restaurant.Contract.DTOs.Identity.Users;
using Restaurant.Domain.Enums;

namespace Restaurant.Contract.DTOs.Personnel.Employees
{
    public class EmployeeResponse
    {
        public string Id { get; set; } = null!;
        public string EmployeeCode { get; set; } = null!;

        public string RoleName { get; set; } = null!;

        public string? AvatarUrl { get; set; }

        public string BranchCode { get; set; } = null!;
        public string PositionCode { get; set; } = null!;

        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }

        public EmployeeStatus Status { get; set; }

        public AccountResponse Account { get; set; } = null!;
        public PersonalProfileResponse PersonalProfile { get; set; } = null!;
    }
}
