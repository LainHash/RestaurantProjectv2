using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Application.Services.Auth;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Catalog;
using Restaurant.Application.Services.Commerce;
using Restaurant.Application.Services.Email;
using Restaurant.Application.Services.Guest;
using Restaurant.Application.Services.Identity;
using Restaurant.Application.Services.Inventory;
using Restaurant.Application.Services.Personnel;
using Restaurant.Application.Services.Pricing;
using Restaurant.Application.Services.Production;
using Restaurant.Application.Services.Storage;
using Restaurant.Application.Services.Territory;
using Restaurant.Contract.Settings.Auth;
using Restaurant.Contract.Settings.Email;
using Restaurant.Contract.Settings.Storage;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.Context;
using Restaurant.Infrastructure.Repositories;
using Restaurant.Infrastructure.Repositories.Catalog;
using Restaurant.Infrastructure.Services.Auth;
using Restaurant.Infrastructure.Services.Business;
using Restaurant.Infrastructure.Services.Catalog;
using Restaurant.Infrastructure.Services.Commerce;
using Restaurant.Infrastructure.Services.Email;
using Restaurant.Infrastructure.Services.Guest;
using Restaurant.Infrastructure.Services.Identity;
using Restaurant.Infrastructure.Services.Inventory;
using Restaurant.Infrastructure.Services.Personnel;
using Restaurant.Infrastructure.Services.Pricing;
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

            // ── Cloudinary ───────────────────────────────────────────────────
            services.Configure<CloudinarySettings>(
                configuration.GetSection(CloudinarySettings.SectionName));
            services.AddScoped<ICloudinaryService, CloudinaryService>();

            // ── Repositories ─────────────────────────────────────────────────
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            var assembly = typeof(ProductCategoryRepository).Assembly;

            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsClass || type.IsAbstract)
                    continue;

                if (!type.Name.EndsWith("Repository"))
                    continue;

                foreach (var iface in type.GetInterfaces())
                {
                    if (iface.Name.EndsWith("Repository"))
                    {
                        services.AddScoped(iface, type);
                    }
                }
            }

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
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IOtpVerificationService, OtpVerificationService>();
            services.AddScoped<IPersonalProfileService, PersonalProfileService>();

            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IWalletService, WalletService>();

            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IPositionService, PositionService>();

            services.AddScoped<IWishlistService, WishlistService>();
            services.AddScoped<ICartService, CartService>();

            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddScoped<IDiscountService, DiscountService>();

            // ── Authentication & Security ────────────────────────────────────
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IOtpHasher, OtpHasher>();

            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            if (jwtSettings is not null)
            {
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                    };
                });
            }

            return services;
        }
    }
}
