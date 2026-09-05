using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Sale;

namespace Restaurant.Infrastructure.Configurations.Sale
{
    internal class OrderPreparationConfiguration
        : IEntityTypeConfiguration<OrderPreparation>
    {
        public void Configure(EntityTypeBuilder<OrderPreparation> builder)
        {
            builder.ToTable("OrderPreparations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityByDefaultColumn();

            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnType("text");

            builder.HasOne(x => x.OrderDetail)
                .WithOne(x => x.OrderPreparation)
                .HasForeignKey<OrderPreparation>(x => x.OrderDetailId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
