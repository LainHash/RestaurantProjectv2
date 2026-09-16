using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Schedule;

namespace Restaurant.Infrastructure.Configurations.Schedule
{
    internal class ReservationConfiguration
        : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.PublicId)
                .IsRequired();

            builder.Property(x => x.BranchId)
                .IsRequired();

            builder.Property(x => x.CustomerId);

            builder.Property(x => x.GuestName)
                .HasMaxLength(100);

            builder.Property(x => x.GuestPhone)
                .HasMaxLength(20);

            builder.Property(x => x.GuestEmail)
                .HasMaxLength(256);

            builder.Property(x => x.ReservationDate)
                .IsRequired();

            builder.Property(x => x.ReservationTime)
                .IsRequired();

            builder.Property(x => x.GuestCount)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.Property(x => x.Note)
                .HasMaxLength(1000);

            builder.Property(x => x.ConfirmedAt);

            builder.Property(x => x.CancelledAt);

            builder.Property(x => x.CancellationReason)
                .HasMaxLength(500);

            // Relationships
            builder.HasOne(x => x.Branch)
                .WithMany(x => x.Reservations)
                .HasForeignKey(x => x.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Reservations)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);

            // Indexes
            builder.HasIndex(x => x.BranchId);
            builder.HasIndex(x => x.CustomerId);
            builder.HasIndex(x => x.Status);
            builder.HasIndex(x => new { x.BranchId, x.ReservationDate, x.ReservationTime });
        }
    }
}
