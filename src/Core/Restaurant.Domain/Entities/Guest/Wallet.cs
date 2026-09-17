using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Guest
{
    public partial class Wallet : SoftDeletableEntity
    {
        public long CustomerId { get; private set; }

        public decimal Balance { get; private set; }

        public bool IsLocked { get; private set; }

        public Customer Customer { get; private set; } = null!;

        public ICollection<WalletTransaction> Transactions { get; private set; } = [];
    }

    public partial class Wallet
    {
        public Wallet() { }
        public Wallet(long customerId)
        {
            CustomerId = customerId;
        }
    }
}
