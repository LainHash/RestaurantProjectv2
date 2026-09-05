using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Restaurant.Application.Services.Auth;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure
{
    internal class RestaurantDbContextFactory : IDesignTimeDbContextFactory<RestaurantDbContext>
    {
        public RestaurantDbContext CreateDbContext(string[] args)
        {
            LoadDotEnv();

            var apiRoot = FindApiProjectRoot();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(apiRoot)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("MyConnectString")
                ?? Environment.GetEnvironmentVariable("ConnectionStrings__MyConnectString");

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException(
                    "Connection string 'MyConnectString' not found. " +
                    "Set it in .env, appsettings.json, or env var 'ConnectionStrings__MyConnectString'.");

            var optionsBuilder = new DbContextOptionsBuilder<RestaurantDbContext>();
            optionsBuilder.UseNpgsql(connectionString,
                o => o.MigrationsAssembly(typeof(RestaurantDbContext).Assembly.FullName));

            var auditContext = new DesignTimeAuditContext();

            return new RestaurantDbContext(optionsBuilder.Options, auditContext);
        }

        private static void LoadDotEnv()
        {
            var searchDir = Directory.GetCurrentDirectory();
            while (searchDir is not null)
            {
                var envPath = Path.Combine(searchDir, ".env");
                if (File.Exists(envPath))
                {
                    foreach (var line in File.ReadAllLines(envPath))
                    {
                        var trimmed = line.Trim();
                        if (string.IsNullOrWhiteSpace(trimmed) || trimmed.StartsWith('#'))
                            continue;

                        var separatorIndex = trimmed.IndexOf('=');
                        if (separatorIndex > 0)
                        {
                            var key = trimmed[..separatorIndex].Trim();
                            var value = trimmed[(separatorIndex + 1)..].Trim();
                            Environment.SetEnvironmentVariable(key, value);
                        }
                    }
                    break;
                }
                searchDir = Directory.GetParent(searchDir)?.FullName;
            }
        }

        private static string FindApiProjectRoot()
        {
            var candidates = new[]
            {
                Path.Combine(Directory.GetCurrentDirectory(), "src", "Restaurant.API"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "Restaurant.API"),
                Directory.GetCurrentDirectory(),
            };

            foreach (var path in candidates)
            {
                if (File.Exists(Path.Combine(path, "appsettings.json")))
                    return path;
            }

            var dir = new DirectoryInfo(Directory.GetCurrentDirectory());
            while (dir != null)
            {
                var found = dir.GetFiles("appsettings.json", SearchOption.AllDirectories)
                    .FirstOrDefault(f => f.Directory?.Name == "Restaurant.API");
                if (found != null)
                    return found.DirectoryName!;
                dir = dir.Parent;
            }

            throw new InvalidOperationException(
                "Cannot locate Restaurant.API/appsettings.json. " +
                "Run the command from the solution root or src/Restaurant.API directory.");
        }

        private sealed class DesignTimeAuditContext : IAuditContext
        {
            public int? UserId { get; set; }
            public string? IpAddress { get; set; }
        }
    }
}
