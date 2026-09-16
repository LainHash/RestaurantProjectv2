namespace Restaurant.Contract.Settings.AuditLog
{
    public class AuditLogSettings
    {
        public const string SectionName = "AuditLogSettings";

        /// <summary>
        /// Số ngày lưu giữ audit log. Log cũ hơn số ngày này sẽ bị xóa tự động.
        /// Mặc định: 90 ngày.
        /// </summary>
        public int RetentionDays { get; set; } = 90;
        public bool Enabled { get; set; } = true;
    }
}
