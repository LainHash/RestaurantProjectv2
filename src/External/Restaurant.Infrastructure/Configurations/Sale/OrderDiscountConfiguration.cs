using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Sale;

namespace Restaurant.Infrastructure.Configurations.Sale
{
    internal class OrderDiscountConfiguration
        : IEntityTypeConfiguration<OrderDiscount>
    {
        public void Configure(EntityTypeBuilder<OrderDiscount> builder)
        {
            builder.ToTable("OrderDiscounts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityByDefaultColumn();

            builder.Property(x => x.DiscountValue)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.DiscountAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasOne(x => x.Order)
                .WithMany(x => x.OrderDiscounts)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Discount)
                .WithMany(x => x.OrderDiscounts)
                .HasForeignKey(x => x.DiscountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
