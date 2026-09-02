namespace Restaurant.Domain.Entities.Business
{
    public class AuditLog
    {
        public long Id { get; private set; }

        /// <summary>UserId của người thực hiện; null nếu là system/anonymous.</summary>
        public int? UserId { get; private set; }

        /// <summary>Hành động: Created | Updated | Deleted</summary>
        public string Action { get; private set; } = string.Empty;

        /// <summary>Tên entity bị tác động, ví dụ: "Product", "Order".</summary>
        public string EntityName { get; private set; } = string.Empty;

        /// <summary>Giá trị Id (int) hoặc PublicId (Guid) của entity.</summary>
        public string EntityId { get; private set; } = string.Empty;

        /// <summary>JSON snapshot các field TRƯỚC khi thay đổi (null với Created).</summary>
        public string? OldValues { get; private set; }

        /// <summary>JSON snapshot các field SAU khi thay đổi (null với Deleted).</summary>
        public string? NewValues { get; private set; }

        /// <summary>IP address của client thực hiện request.</summary>
        public string? IpAddress { get; private set; }

        /// <summary>Thời điểm UTC ghi nhận thay đổi.</summary>
        public DateTime Timestamp { get; private set; }

        private AuditLog() { }

        public static AuditLog Create(
            int? userId,
            string action,
            string entityName,
            string entityId,
            string? oldValues,
            string? newValues,
            string? ipAddress,
            DateTime timestamp)
        {
            return new AuditLog
            {
                UserId = userId,
                Action = action,
                EntityName = entityName,
                EntityId = entityId,
                OldValues = oldValues,
                NewValues = newValues,
                IpAddress = ipAddress,
                Timestamp = timestamp
            };
        }
    }
}
