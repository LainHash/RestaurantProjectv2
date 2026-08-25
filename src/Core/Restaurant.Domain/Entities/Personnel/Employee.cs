using NanoidDotNet;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Personnel
{
    public partial class Employee : SoftDeletableEntity
    {
        public string EmployeeCode { get; private set; } = Nanoid.Generate(size: 10);

        public int UserId { get; private set; }
        public User User { get; private set; } = null!;

        public int PositionId { get; private set; }
        public Position Position { get; private set; } = null!;

        public DateTime HireDate { get; private set; }
        public DateTime? TerminationDate { get; private set; }

        public EmployeeStatus Status { get; private set; }

        public int? AvatarImageId { get; private set; }
        public Image? AvatarImage { get; private set; } = null!;
    }

    public partial class Employee
    {
        public Employee() { }

        public Employee SetUser(int userId)
        {
            UserId = userId;
            return this;
        }

        public Employee SetPosition(int positionId)
        {
            PositionId = positionId;
            return this;
        }

        public Employee SetAvatar(int imageId)
        {
            AvatarImageId = imageId;
            return this;
        }
        public Employee ClearAvatar()
        {
            AvatarImageId = null;
            return this;
        }
    }
}
