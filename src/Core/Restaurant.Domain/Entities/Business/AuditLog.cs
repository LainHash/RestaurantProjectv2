namespace Restaurant.Domain.Entities.Business
{
    public class AuditLog
    {
        public long Id { get; private set; }

        public long? UserId { get; private set; }

        public string Action { get; private set; } = string.Empty;

        public string EntityName { get; private set; } = string.Empty;

        public string EntityId { get; private set; } = string.Empty;

        public string? OldValues { get; private set; }

        public string? NewValues { get; private set; }

        public string? IpAddress { get; private set; }

        public DateTime Timestamp { get; private set; }

        private AuditLog() { }

        public static AuditLog Create(
            long? userId,
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
