using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Billing;

namespace Restaurant.Infrastructure.Configurations.Billing
{
    internal class PaymentConfiguration
        : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityByDefaultColumn();

            builder.Property(x => x.PublicId)
                .IsRequired();

            builder.Property(x => x.InvoiceId)
                .IsRequired();

            builder.Property(x => x.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.Currency)
                .IsRequired()
                .HasMaxLength(10)
                .HasDefaultValue("VND");

            builder.Property(x => x.Method)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnType("text");

            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnType("text");

            builder.Property(x => x.PaidAt);

            builder.HasOne(x => x.Invoice)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.PaymentTransactions)
                .WithOne(x => x.Payment)
                .HasForeignKey(x => x.PaymentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.InvoiceId);

            builder.HasIndex(x => x.Status);

            builder.HasIndex(x => x.Method);
        }
    }
}
