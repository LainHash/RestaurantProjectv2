using Restaurant.Application.Services.Auth;

namespace Restaurant.Infrastructure.Services.Auth
{
    internal class AuditContext : IAuditContext
    {
        public long? UserId { get; set; }
        public string? IpAddress { get; set; }
    }
}
