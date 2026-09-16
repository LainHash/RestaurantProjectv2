using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Schedule;

namespace Restaurant.Infrastructure.Configurations.Schedule
{
    internal class ReservationTableConfiguration
        : IEntityTypeConfiguration<ReservationTable>
    {
        public void Configure(EntityTypeBuilder<ReservationTable> builder)
        {
            builder.ToTable("ReservationTables");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.PublicId)
                .IsRequired();

            builder.Property(x => x.ReservationId)
                .IsRequired();

            builder.Property(x => x.RestaurantTableId)
                .IsRequired();

            // Relationships
            builder.HasOne(x => x.Reservation)
                .WithMany(x => x.ReservationTables)
                .HasForeignKey(x => x.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.RestaurantTable)
                .WithMany()
                .HasForeignKey(x => x.RestaurantTableId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(x => new { x.ReservationId, x.RestaurantTableId })
                .IsUnique();
            builder.HasIndex(x => x.RestaurantTableId);
        }
    }
}
