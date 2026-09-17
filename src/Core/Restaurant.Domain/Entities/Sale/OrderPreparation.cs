using Restaurant.Domain.Enums;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Sale
{
    public partial class OrderPreparation : SoftDeletableEntity
    {
        public long OrderDetailId { get; private set; }
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

        public void Preparing()
        {
            Status = PreparationStatus.Preparing;
            StartedAt = DateTime.UtcNow;
        }

        public void Ready()
        {
            Status = PreparationStatus.Ready;
        }

        public void Served()
        {
            Status = PreparationStatus.Served;
            CompletedAt = DateTime.UtcNow;
        }

        public void Cancelled()
        {
            Status = PreparationStatus.Cancelled;
        }
    }
}
