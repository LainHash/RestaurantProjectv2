using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Personnel
{
    public partial class Position : SoftDeletableEntity
    {
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public string PositionCode { get; private set; } = null!;

        public long DepartmentId { get; private set; }
        public Department Department { get; private set; } = null!;

        public ICollection<Employee> Employees { get; private set; } = [];
    }

    public partial class Position
    {
        public Position() { }


        public Position SetDepartment(long departmentId)
        {
            DepartmentId = departmentId;
            return this;
        }
    }
}
