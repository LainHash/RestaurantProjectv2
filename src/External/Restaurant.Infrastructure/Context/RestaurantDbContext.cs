using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Restaurant.Application.Services.Auth;
using Restaurant.Domain.Entities.Billing;
using Restaurant.Domain.Entities.Business;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Entities.Pricing;
using Restaurant.Domain.Entities.Production;
using Restaurant.Domain.Entities.Sale;
using Restaurant.Domain.Entities.Schedule;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Models;
using System.Reflection;
using System.Text.Json;

namespace Restaurant.Infrastructure.Context
{
    public class RestaurantDbContext : DbContext
    {
        public DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public DbSet<IngredientCategory> IngredientCategories { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;

        public DbSet<Unit> Units { get; set; } = null!;

        public DbSet<Branch> Branches { get; set; } = null!;
        public DbSet<Area> Areas { get; set; } = null!;
        public DbSet<RestaurantTable> RestaurantTables { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductPrice> ProductPrices { get; set; } = null!;
        public DbSet<ProductStock> ProductStocks { get; set; } = null!;

        public DbSet<Image> Images { get; set; } = null!;
        public DbSet<ProductImage> ProductImages { get; set; } = null!;
        public DbSet<ProductCategoryImage> ProductCategoryImages { get; set; } = null!;
        public DbSet<BrandImage> BrandImages { get; set; } = null!;

        public DbSet<Ingredient> Ingredients { get; set; } = null!;
        public DbSet<IngredientPrice> IngredientPrices { get; set; } = null!;
        public DbSet<IngredientStock> IngredientStocks { get; set; } = null!;

        public DbSet<Recipe> Recipes { get; set; } = null!;
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; } = null!;

        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<OtpVerification> OtpVerifications { get; set; } = null!;
        public DbSet<PersonalProfile> PersonalProfiles { get; set; } = null!;
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;

        public DbSet<Wallet> Wallets { get; set; } = null!;

        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Position> Positions { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;

        public DbSet<Wishlist> Wishlists { get; set; } = null!;
        public DbSet<WishlistItem> WishlistItems { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; } = null!;

        public DbSet<Discount> Discounts { get; set; } = null!;
        public DbSet<DiscountCustomer> DiscountsCustomer { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public DbSet<OrderPreparation> OrderPreparation { get; set; } = null!;

        public DbSet<Invoice> Invoices { get; set; } = null!;
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; } = null!;

        public DbSet<Reservation> Reservations { get; set; } = null!;
        public DbSet<ReservationTable> ReservationTables { get; set; } = null!;

        public DbSet<AuditLog> AuditLogs { get; set; } = null!;

        // ── IAuditContext injected via constructor ────────────────────────────
        private readonly IAuditContext _auditContext;

        public RestaurantDbContext(
            DbContextOptions<RestaurantDbContext> options,
            IAuditContext auditContext)
            : base(options)
        {
            _auditContext = auditContext;
        }

        // ── Model building ──────────────────────────────────────────────────
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Auto-register all IEntityTypeConfiguration<T> classes in this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        // ── Auto-set audit fields on SaveChanges ────────────────────────────
        public override int SaveChanges()
        {
            SetAuditFields();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditFields();

            var auditEntries = CaptureAuditEntries();

            var result = await base.SaveChangesAsync(cancellationToken);

            // Sau khi save, flush audit logs (Id của entity Added giờ đã có giá trị)
            if (auditEntries.Count > 0)
            {
                FinalizeAuditEntries(auditEntries);
                AuditLogs.AddRange(auditEntries.Select(e => (AuditLog)e));
                await base.SaveChangesAsync(cancellationToken);
            }

            return result;
        }

        // ── Internal helpers ────────────────────────────────────────────────
        private void SetAuditFields()
        {
            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.MarkCreated(now);
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.MarkUpdated(now);
                }
            }
        }

        private List<AuditEntry> CaptureAuditEntries()
        {
            ChangeTracker.DetectChanges();

            var auditEntries = new List<AuditEntry>();
            var timestamp = DateTime.UtcNow;

            var trackedStates = new[] { EntityState.Added, EntityState.Modified, EntityState.Deleted };

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog) continue;
                if (!trackedStates.Contains(entry.State)) continue;

                var auditEntry = new AuditEntry(entry)
                {
                    UserId = _auditContext.UserId,
                    IpAddress = _auditContext.IpAddress,
                    EntityName = entry.Metadata.ClrType.Name,
                    Timestamp = timestamp,
                    Action = entry.State switch
                    {
                        EntityState.Added => "Created",
                        EntityState.Modified => "Updated",
                        EntityState.Deleted => "Deleted",
                        _ => "Unknown"
                    }
                };

                foreach (var prop in entry.Properties)
                {
                    if (prop.Metadata.IsShadowProperty()) continue;

                    var propName = prop.Metadata.Name;

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propName] = prop.CurrentValue;
                            if (prop.Metadata.IsPrimaryKey())
                                auditEntry.IsTemporaryId = true;
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[propName] = prop.OriginalValue;
                            auditEntry.EntityId = GetEntityId(entry);
                            break;

                        case EntityState.Modified:
                            if (prop.IsModified)
                            {
                                auditEntry.OldValues[propName] = prop.OriginalValue;
                                auditEntry.NewValues[propName] = prop.CurrentValue;
                            }
                            auditEntry.EntityId = GetEntityId(entry);
                            break;
                    }
                }

                auditEntries.Add(auditEntry);
            }

            return auditEntries;
        }

        private static void FinalizeAuditEntries(List<AuditEntry> auditEntries)
        {
            foreach (var entry in auditEntries.Where(e => e.IsTemporaryId))
            {
                entry.EntityId = GetEntityId(entry.DbEntry);
            }
        }

        private static string GetEntityId(EntityEntry entry)
        {
            var publicIdProp = entry.Properties
                .FirstOrDefault(p => p.Metadata.Name == "PublicId");

            if (publicIdProp?.CurrentValue is not null)
                return publicIdProp.CurrentValue.ToString()!;

            var pkProp = entry.Properties
                .FirstOrDefault(p => p.Metadata.IsPrimaryKey());

            return pkProp?.CurrentValue?.ToString() ?? string.Empty;
        }

        // ── Helper nested class ──────────────────────────────────────────────
        private sealed class AuditEntry(EntityEntry dbEntry)
        {
            public EntityEntry DbEntry { get; } = dbEntry;
            public int? UserId { get; set; }
            public string? IpAddress { get; set; }
            public string EntityName { get; set; } = string.Empty;
            public string EntityId { get; set; } = string.Empty;
            public string Action { get; set; } = string.Empty;
            public DateTime Timestamp { get; set; }
            public bool IsTemporaryId { get; set; }
            public Dictionary<string, object?> OldValues { get; } = [];
            public Dictionary<string, object?> NewValues { get; } = [];

            public static implicit operator AuditLog(AuditEntry entry)
                => AuditLog.Create(
                    userId: entry.UserId,
                    action: entry.Action,
                    entityName: entry.EntityName,
                    entityId: entry.EntityId,
                    oldValues: entry.OldValues.Count > 0
                        ? JsonSerializer.Serialize(entry.OldValues)
                        : null,
                    newValues: entry.NewValues.Count > 0
                        ? JsonSerializer.Serialize(entry.NewValues)
                        : null,
                    ipAddress: entry.IpAddress,
                    timestamp: entry.Timestamp);
        }
    }
}
