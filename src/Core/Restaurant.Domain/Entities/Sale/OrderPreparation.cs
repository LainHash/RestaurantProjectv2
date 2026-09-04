using Restaurant.Domain.Enums;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Sale
{
    public partial class OrderPreparation : SoftDeletableEntity
    {
        public int OrderDetailId { get; private set; }
        public PreparationStatus Status { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }

        public OrderDetail OrderDetail { get; private set; } = null!;
    }

    public partial class OrderPreparation
    {
        public OrderPreparation()
        {
            Status = PreparationStatus.Pending;
        }
    }
}
