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
            CreateMap<IngredientRecord, Ingredient>()
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore())
                .ForMember(dest => dest.BrandId, opt => opt.Ignore())
                .ForMember(dest => dest.BaseUnitId, opt => opt.Ignore());
            CreateMap<ProductCategoryRecord, ProductCategory>();
            CreateMap<ProductRecord, Product>()
                .ForMember(dest => dest.InventoryType, opt => opt
                    .MapFrom(src => Enum.Parse<InventoryType>(src.InventoryType)))
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore())
                .ForMember(dest => dest.BrandId, opt => opt.Ignore())
                .ForMember(dest => dest.UnitId, opt => opt.Ignore());

            CreateMap<CustomerRecord, Customer>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<PersonalProfileRecord, PersonalProfile>()
                .ForMember(dest => dest.DateOfBirth, opt => opt
                    .MapFrom(src => DateOnly.FromDateTime(src.DateOfBirth)))
                .ForMember(dest => dest.UserId, opt => opt.Ignore());
            CreateMap<RoleRecord, Role>();
            CreateMap<UserRecord, User>()
                .ForMember(dest => dest.RoleId, opt => opt.Ignore());

            CreateMap<IngredientStockRecord, IngredientStock>()
                .ForMember(dest => dest.IngredientId, opt => opt.Ignore())
                .ForMember(dest => dest.BranchId, opt => opt.Ignore());
            CreateMap<ProductStockRecord, ProductStock>()
                .ForMember(dest => dest.ProductId, opt => opt.Ignore())
                .ForMember(dest => dest.BranchId, opt => opt.Ignore());
            CreateMap<UnitRecord, Unit>()
                .ForMember(dest => dest.Type, opt => opt
                    .MapFrom(src => Enum.Parse<UnitType>(src.Type)));

            CreateMap<DepartmentRecord, Department>();
            CreateMap<EmployeeRecord, Employee>()
                .ForMember(dest => dest.Status, opt => opt
                    .MapFrom(src => Enum.Parse<EmployeeStatus>(src.Status)))
                .ForMember(dest => dest.HireDate, opt => opt
                    .MapFrom(src => src.HireDate.ToUniversalTime()))
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.BranchId, opt => opt.Ignore())
                .ForMember(dest => dest.PositionId, opt => opt.Ignore());
            CreateMap<PositionRecord, Position>()
                .ForMember(dest => dest.DepartmentId, opt => opt.Ignore());

            CreateMap<DiscountRecord, Discount>()
                .ForMember(dest => dest.StartAt, opt => opt
                    .MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.EndAt, opt => opt
                    .MapFrom(src => DateTime.UtcNow.AddYears(5)));
            CreateMap<IngredientPriceRecord, IngredientPrice>()
                .ForMember(dest => dest.IngredientId, opt => opt.Ignore());
            CreateMap<ProductPriceRecord, ProductPrice>()
                .ForMember(dest => dest.ProductId, opt => opt.Ignore());

            CreateMap<RecipeIngredientRecord, RecipeIngredient>()
                .ForMember(dest => dest.RecipeId, opt => opt.Ignore())
                .ForMember(dest => dest.IngredientId, opt => opt.Ignore())
                .ForMember(dest => dest.UnitId, opt => opt.Ignore());
            CreateMap<RecipeRecord, Recipe>()
                .ForMember(dest => dest.ProductId, opt => opt.Ignore());

            CreateMap<ImageRecord, Image>();
            CreateMap<ProductImageRecord, ProductImage>()
                .ForMember(dest => dest.ProductId, opt => opt.Ignore())
                .ForMember(dest => dest.ImageId, opt => opt.Ignore());
            CreateMap<ProductCategoryImageRecord, ProductCategoryImage>()
                .ForMember(dest => dest.ImageId, opt => opt.Ignore())
                .ForMember(dest => dest.ProductCategoryId, opt => opt.Ignore());
            CreateMap<BrandImageRecord, BrandImage>()
                .ForMember(dest => dest.ImageId, opt => opt.Ignore())
                .ForMember(dest => dest.BrandId, opt => opt.Ignore());

            CreateMap<BranchRecord, Branch>()
                .ForMember(dest => dest.Status, opt => opt
                    .MapFrom(src => Enum.Parse<BranchStatus>(src.Status)))
                .ForMember(dest => dest.OpenTime, opt => opt
                    .MapFrom(src => TimeOnly.FromDateTime(src.OpenTime)))
                .ForMember(dest => dest.CloseTime, opt => opt
                    .MapFrom(src => TimeOnly.FromDateTime(src.CloseTime)));
        }
    }
}
