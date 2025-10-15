namespace Chat.Domain.Abstractions
{
    public interface IJwtProvider
    {
        string GenerateToken(string email);
    }
}