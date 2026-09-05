namespace Restaurant.Contract.DTOs.Business.AuditLogs
{
    /// <summary>
    /// Response DTO cho một audit log entry.
    /// </summary>
    public record AuditLogResponse(
        long Id,
        int? UserId,
        string Action,
        string EntityName,
        string EntityId,
        string? OldValues,
        string? NewValues,
        string? IpAddress,
        DateTime Timestamp);
}
