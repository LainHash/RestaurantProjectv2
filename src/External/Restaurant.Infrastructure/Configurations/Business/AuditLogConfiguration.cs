using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Business;

namespace Restaurant.Infrastructure.Configurations.Business
{
    internal class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityByDefaultColumn();

            builder.Property(x => x.Action)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.EntityName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.EntityId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.OldValues)
                .HasColumnType("jsonb");

            builder.Property(x => x.NewValues)
                .HasColumnType("jsonb");

            builder.Property(x => x.IpAddress)
                .HasMaxLength(45); // IPv6 max length

            builder.Property(x => x.Timestamp)
                .IsRequired();

            // Index để query nhanh theo entity, user, thời gian
            builder.HasIndex(x => x.EntityName);
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.Timestamp);
            builder.HasIndex(x => new { x.EntityName, x.EntityId });
        }
    }
}
