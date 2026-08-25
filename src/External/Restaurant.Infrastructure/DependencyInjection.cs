using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Services.Auth;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Catalog;
using Restaurant.Application.Services.Commerce;
using Restaurant.Application.Services.Guest;
using Restaurant.Application.Services.Identity;
using Restaurant.Application.Services.Inventory;
using Restaurant.Application.Services.Personnel;
using Restaurant.Application.Services.Production;
using Restaurant.Application.Services.Storage;
using Restaurant.Application.Services.Territory;
using Restaurant.Infrastructure.Context;
using Restaurant.Infrastructure.Services.Business;
using Restaurant.Infrastructure.Services.Auth;
using Restaurant.Infrastructure.Services.Catalog;
using Restaurant.Infrastructure.Services.Commerce;
using Restaurant.Infrastructure.Services.Guest;
using Restaurant.Infrastructure.Services.Identity;
using Restaurant.Infrastructure.Services.Inventory;
using Restaurant.Infrastructure.Services.Personnel;
using Restaurant.Infrastructure.Services.Production;
using Restaurant.Infrastructure.Services.Storage;
using Restaurant.Infrastructure.Services.Territory;

namespace Restaurant.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // ── Database ─────────────────────────────────────────────────────
            services.AddDbContext<RestaurantDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("MyConnectString"),
                    sqlOptions => sqlOptions.MigrationsAssembly(
                        typeof(RestaurantDbContext).Assembly.FullName)));

            // ── AutoMapper ───────────────────────────────────────────────────
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(DependencyInjection).Assembly));

            // ── Repositories ─────────────────────────────────────────────────
            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            //var assembly = typeof(ProductCategoryRepository).Assembly;

            //foreach (var type in assembly.GetTypes())
            //{
            //    if (!type.IsClass || type.IsAbstract)
            //        continue;

            //    if (!type.Name.EndsWith("Repository"))
            //        continue;

            //    foreach (var iface in type.GetInterfaces())
            //    {
            //        if (iface.Name.EndsWith("Repository"))
            //        {
            //            services.AddScoped(iface, type);
            //        }
            //    }
            //}

            // ── Services ─────────────────────────────────────────────────────
            services.AddScoped<IDataImporter, ExcelImporter>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IIngredientCategoryService, IngredientCategoryService>();
            services.AddScoped<IBrandService, BrandService>();

            services.AddScoped<IBranchService, BranchService>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IIngredientService, IngredientService>();

            services.AddScoped<IProductStockService, ProductStockService>();
            services.AddScoped<IIngredientStockService, IngredientStockService>();

            services.AddScoped<IImageService, ImageService>();

            services.AddScoped<IRecipeService, RecipeService>();

            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IOtpVerificationService, OtpVerificationService>();
            services.AddScoped<IPersonalProfileService, PersonalProfileService>();

            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IWalletService, WalletService>();

            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IPositionService, PositionService>();

            services.AddScoped<IWishlistService, WishlistService>();
            services.AddScoped<ICartService, CartService>();

            services.AddScoped<IEmployeeService, EmployeeService>();

            return services;
        }
    }
}
