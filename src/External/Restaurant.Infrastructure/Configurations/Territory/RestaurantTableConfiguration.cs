using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Territory;

namespace Restaurant.Infrastructure.Configurations.Territory
{
    internal class RestaurantTableConfiguration
        : IEntityTypeConfiguration<RestaurantTable>
    {
        public void Configure(EntityTypeBuilder<RestaurantTable> builder)
        {
            builder.ToTable("RestaurantTables");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.PublicId)
                .IsRequired();

            builder.Property(x => x.AreaId)
                .IsRequired();

            builder.Property(x => x.TableNumber)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Capacity)
                .IsRequired();

            builder.Property(x => x.Shape)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .IsRequired();

            // Indexes
            builder.HasIndex(x => new { x.AreaId, x.TableNumber })
                .IsUnique();
            builder.HasIndex(x => x.Status);
        }
    }
}
