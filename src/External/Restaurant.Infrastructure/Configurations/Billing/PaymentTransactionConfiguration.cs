using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Billing;

namespace Restaurant.Infrastructure.Configurations.Billing
{
    internal class PaymentTransactionConfiguration
        : IEntityTypeConfiguration<PaymentTransaction>
    {
        public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
        {
            builder.ToTable("PaymentTransactions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityByDefaultColumn();

            builder.Property(x => x.PublicId)
                .IsRequired();

            builder.Property(x => x.PaymentId)
                .IsRequired();

            builder.Property(x => x.Provider)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.TransactionCode)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnType("text");

            builder.Property(x => x.RequestData)
                .HasColumnType("text");

            builder.Property(x => x.ResponseData)
                .HasColumnType("text");

            builder.Property(x => x.CompletedAt);

            builder.HasOne(x => x.Payment)
                .WithMany(x => x.PaymentTransactions)
                .HasForeignKey(x => x.PaymentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.PaymentId);

            builder.HasIndex(x => new { x.Provider, x.TransactionCode });

            builder.HasIndex(x => x.Status);
        }
    }
}
