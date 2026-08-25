namespace Restaurant.Application.Services.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(Guid userId, string userName, string email, string role);
    }
}
