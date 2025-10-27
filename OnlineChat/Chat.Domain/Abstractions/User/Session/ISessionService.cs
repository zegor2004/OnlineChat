namespace Chat.Domain.Abstractions.User.Session
{
    public interface ISessionService
    {
        Task<List<string>> GetSessionUserByUserId(Guid userId);
    }
}