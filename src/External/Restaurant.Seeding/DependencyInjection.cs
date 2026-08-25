using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Infrastructure.Context;
using Restaurant.Seeding.Seeders;

namespace Restaurant.Seeding
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSeeding(this IServiceCollection services)
        {
            // ── AutoMapper — chỉ scan Seeding assembly (SeedMapping profiles) ─
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(DependencyInjection).Assembly));

            // ── DatabaseSeeder orchestrator ───────────────────────────────────
            services.AddScoped<DatabaseSeeder>();

            // ── Auto-register all IDataSeeder implementations ─────────────────
            var seederTypes = typeof(DependencyInjection).Assembly.GetTypes()
                .Where(t => typeof(IDataSeeder).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var type in seederTypes)
                services.AddScoped(type);

            return services;
        }

        public static async Task InitialiseDatabaseAsync(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var sp = scope.ServiceProvider;

            var context = sp.GetRequiredService<RestaurantDbContext>();
            await context.Database.MigrateAsync();

            var seeder = sp.GetRequiredService<DatabaseSeeder>();
            await seeder.SeedAllAsync();
        }
    }
}
