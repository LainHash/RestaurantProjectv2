using Restaurant.Application.Services.Auth;

namespace Restaurant.Infrastructure.Services.Auth
{
    /// <summary>
    /// Scoped implementation của IAuditContext.
    /// Được inject vào RestaurantDbContext và AuditLogBehavior trong cùng scope.
    /// </summary>
    internal class AuditContext : IAuditContext
    {
        public long? UserId { get; set; }
        public string? IpAddress { get; set; }
    }
}
