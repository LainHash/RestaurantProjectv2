using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Pricing;

namespace Restaurant.Infrastructure.Configurations.Pricing
{
    internal class DiscountCustomerConfiguration
        : IEntityTypeConfiguration<DiscountCustomer>
    {
        public void Configure(EntityTypeBuilder<DiscountCustomer> builder)
        {
            builder.ToTable("DiscountCustomers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityByDefaultColumn();

            builder.Property(x => x.PublicId)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.HasIndex(x => new { x.CustomerId, x.DiscountId })
                .IsUnique();

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.DiscountCustomers)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Discount)
                .WithMany(x => x.DiscountCustomers)
                .HasForeignKey(x => x.DiscountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
