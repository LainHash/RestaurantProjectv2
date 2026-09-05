namespace Restaurant.Application.Services.Auth
{
    /// <summary>
    /// Cung cấp thông tin về người dùng đang thực hiện request hiện tại.
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>UserId (internal int PK) của người dùng; null nếu chưa xác thực.</summary>
        int? UserId { get; }

        /// <summary>IP address của client; null nếu không xác định được.</summary>
        string? IpAddress { get; }
    }
}
