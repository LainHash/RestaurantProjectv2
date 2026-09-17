using Restaurant.Domain.Enums;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Guest
{
    public class WalletTransaction : SoftDeletableEntity
    {
        public long WalletId { get; private set; }

        public decimal Amount { get; private set; }

        public decimal BalanceBefore { get; private set; }

        public decimal BalanceAfter { get; private set; }

        public WalletTransactionType Type { get; private set; }

        public WalletTransactionStatus Status { get; private set; }

        public string? Description { get; private set; }

        public long? ReferenceId { get; private set; }
        public WalletReferenceType? ReferenceType { get; private set; }

        public Wallet Wallet { get; private set; } = null!;
    }
}
