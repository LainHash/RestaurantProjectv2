using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Territory;

namespace Restaurant.Infrastructure.Configurations.Territory
{
    internal class AreaConfiguration
        : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.ToTable("Areas");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.PublicId)
                .IsRequired();

            builder.Property(x => x.BranchId)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.DisplayOrder)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .IsRequired();

            // Relationships
            builder.HasOne(x => x.Branch)
                .WithMany(x => x.Areas)
                .HasForeignKey(x => x.BranchId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.RestaurantTables)
                .WithOne(x => x.Area)
                .HasForeignKey(x => x.AreaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.BranchId, x.Name })
                .IsUnique();
        }
    }
}
