namespace Chat.Domain.Abstractions.User.Session
{
    public interface ISessionService
    {
        Task<List<string>> GetUserSessions(Guid userId);
        Task CreateSession(string connectionId, Guid userId);
        Task DeleteSession(string connectionId);
    }
}