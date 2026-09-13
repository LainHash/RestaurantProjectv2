namespace Restaurant.Application.Services.Auth
{
    public interface ICurrentUserService
    {
        int? UserId { get; }

        Guid? PublicId { get; }

        string? IpAddress { get; }
    }
}
