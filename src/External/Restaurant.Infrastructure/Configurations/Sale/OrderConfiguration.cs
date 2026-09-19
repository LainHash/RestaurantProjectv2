using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Sale;

namespace Restaurant.Infrastructure.Configurations.Sale
{
    internal class OrderConfiguration
        : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityByDefaultColumn();

            builder.Property(x => x.OrderCode)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.CustomerId);

            builder.Property(x => x.EmployeeId);

            builder.Property(x => x.BranchId)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnType("text");

            builder.Property(x => x.Type)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnType("text");

            builder.Property(x => x.Subtotal)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.DiscountAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.TaxAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.DeliveryFee)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.Note)
                .HasMaxLength(1000);

            builder.Property(x => x.DeliveryAddress)
                .HasMaxLength(500);

            builder.Property(x => x.RestaurantTableId);

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Employee)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Branch)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.OrderDetails)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.RestaurantTable)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.RestaurantTableId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(x => x.OrderCode)
                .IsUnique();
        }
    }
}
