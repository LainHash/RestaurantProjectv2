using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities.Storage;

namespace Restaurant.Infrastructure.Configurations.Storage
{
    internal class ProductCategoryImageConfiguration
        : IEntityTypeConfiguration<ProductCategoryImage>
    {
        public void Configure(EntityTypeBuilder<ProductCategoryImage> builder)
        {
            builder.ToTable("ProductCategoryImages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityByDefaultColumn();

            builder.HasOne(x => x.Image)
                .WithOne(x => x.ProductCategoryImage)
                .HasForeignKey<ProductCategoryImage>(x => x.ImageId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ProductCategory)
                .WithMany(x => x.ProductCategoryImages)
                .HasForeignKey(x => x.ProductCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
