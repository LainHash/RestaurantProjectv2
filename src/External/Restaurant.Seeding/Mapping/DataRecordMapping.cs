using AutoMapper;
using CloudinaryDotNet.Core;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Entities.Pricing;
using Restaurant.Domain.Entities.Production;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Enums;
using Restaurant.Seeding.DataRecords.Catalog;
using Restaurant.Seeding.DataRecords.Guest;
using Restaurant.Seeding.DataRecords.Identity;
using Restaurant.Seeding.DataRecords.Inventory;
using Restaurant.Seeding.DataRecords.Personnel;
using Restaurant.Seeding.DataRecords.Pricing;
using Restaurant.Seeding.DataRecords.Production;
using Restaurant.Seeding.DataRecords.Storage;
using Restaurant.Seeding.DataRecords.Territory;

namespace Restaurant.Seeding.Mapping
{
    internal class DataRecordMapping : Profile
    {
        public DataRecordMapping()
        {
            CreateMap<BrandRecord, Brand>();
            CreateMap<IngredientCategoryRecord, IngredientCategory>();
            CreateMap<IngredientRecord, Ingredient>();
            CreateMap<ProductCategoryRecord, ProductCategory>();
            CreateMap<ProductRecord, Product>()
                .ForMember(dest => dest.InventoryType, opt => opt
                    .MapFrom(src => Enum.Parse<InventoryType>(src.InventoryType)));

            CreateMap<CustomerRecord, Customer>();

            CreateMap<PersonalProfileRecord, PersonalProfile>()
                .ForMember(dest => dest.DateOfBirth, opt => opt
                    .MapFrom(src => DateOnly.FromDateTime(src.DateOfBirth)));
            CreateMap<RoleRecord, Role>();
            CreateMap<UserRecord, User>();

            CreateMap<IngredientStockRecord, IngredientStock>();
            CreateMap<ProductStockRecord, ProductStock>();
            CreateMap<UnitRecord, Unit>()
                .ForMember(dest => dest.Type, opt => opt
                    .MapFrom(src => Enum.Parse<UnitType>(src.Type)));

            CreateMap<DepartmentRecord, Department>();
            CreateMap<EmployeeRecord, Employee>()
                .ForMember(dest => dest.Status, opt => opt
                    .MapFrom(src => Enum.Parse<EmployeeStatus>(src.Status)))
                .ForMember(dest => dest.HireDate, opt => opt
                    .MapFrom(src => src.HireDate.ToUniversalTime()));
            CreateMap<PositionRecord, Position>();

            CreateMap<DiscountRecord, Discount>()
                .ForMember(dest => dest.StartAt, opt => opt
                    .MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.EndAt, opt => opt
                    .MapFrom(src => DateTime.UtcNow.AddYears(5)));
            CreateMap<IngredientPriceRecord, IngredientPrice>();
            CreateMap<ProductPriceRecord, ProductPrice>();

            CreateMap<RecipeIngredientRecord, RecipeIngredient>();
            CreateMap<RecipeRecord, Recipe>();

            CreateMap<ImageRecord, Image>();
            CreateMap<ProductImageRecord, ProductImage>();
            CreateMap<BrandImageRecord, BrandImage>();

            CreateMap<BranchRecord, Branch>()
                .ForMember(dest => dest.Status, opt => opt
                    .MapFrom(src => Enum.Parse<BranchStatus>(src.Status)))
                .ForMember(dest => dest.OpenTime, opt => opt
                    .MapFrom(src => TimeOnly.FromDateTime(src.OpenTime)))
                .ForMember(dest => dest.CloseTime, opt => opt
                    .MapFrom(src => TimeOnly.FromDateTime(src.CloseTime)));
            CreateMap<AreaRecord, Area>();
            CreateMap<RestaurantTableRecord, RestaurantTable>();
        }
    }
}
