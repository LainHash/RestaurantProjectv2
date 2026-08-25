using AutoMapper;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Seeding.DataRecords.Catalog;

namespace Restaurant.Seeding.Mapping.Catalog
{
    internal class ProductCategorySeedMapping : Profile
    {
        public ProductCategorySeedMapping()
        {
            CreateMap<ProductCategoryRecord, ProductCategory>();
        }
    }
}
