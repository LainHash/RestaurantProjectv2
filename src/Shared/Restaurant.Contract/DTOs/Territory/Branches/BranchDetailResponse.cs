using Restaurant.Contract.DTOs.Territory.Areas;
using Restaurant.Domain.Enums;

namespace Restaurant.Contract.DTOs.Territory.Branches
{
    public class BranchDetailResponse
    {
        public Guid Id { get; set; }
        public string City { get; set; } = null!;
        public string Code { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string Address { get; set; } = null!;

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public BranchStatus Status { get; set; }

        public TimeOnly OpenTime { get; set; }
        public TimeOnly CloseTime { get; set; }

        public IEnumerable<AreaResponse> Areas { get; set; } = [];
    }
}
