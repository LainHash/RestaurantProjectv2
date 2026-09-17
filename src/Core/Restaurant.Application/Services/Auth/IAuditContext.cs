namespace Restaurant.Application.Services.Auth
{
    /// <summary>
    /// Scoped context chứa thông tin audit của request hiện tại.
    /// Được set bởi AuditLogBehavior và đọc bởi RestaurantDbContext.
    /// </summary>
    public interface IAuditContext
    {
        long? UserId { get; set; }
        string? IpAddress { get; set; }
    }
}
