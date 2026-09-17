using NanoidDotNet;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Entities.Sale;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Personnel
{
    public partial class Employee : SoftDeletableEntity
    {
        public string EmployeeCode { get; private set; } = Nanoid.Generate("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", 20);

        public long UserId { get; private set; }
        public User User { get; private set; } = null!;

        public long PositionId { get; private set; }
        public Position Position { get; private set; } = null!;

        public long BranchId { get; private set; }
        public Branch Branch { get; private set; } = null!;

        public DateTime HireDate { get; private set; }
        public DateTime? TerminationDate { get; private set; }

        public EmployeeStatus Status { get; private set; }

        public long? AvatarImageId { get; private set; }
        public Image? AvatarImage { get; private set; } = null!;
        public ICollection<Order> Orders { get; private set; } = [];
    }

    public partial class Employee
    {
        public Employee() { }

        public Employee SetUser(long userId)
        {
            UserId = userId;
            return this;
        }

        public Employee SetPosition(long positionId)
        {
            PositionId = positionId;
            return this;
        }

        public Employee SetAvatar(long imageId)
        {
            AvatarImageId = imageId;
            return this;
        }

        public Employee SetBranch(long branchId)
        {
            BranchId = branchId;
            return this;
        }
        public Employee ClearAvatar()
        {
            AvatarImageId = null;
            return this;
        }
    }
}
