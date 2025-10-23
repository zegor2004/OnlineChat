namespace Chat.Domain.Abstractions.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(string userId);
    }
}