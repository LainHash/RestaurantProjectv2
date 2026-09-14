namespace Restaurant.Application.Services.Auth
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }

        string? IpAddress { get; }
    }
}
