namespace Chat.Domain.Abstractions.User.Session
{
    public interface ISessionRepository
    {
        Task<bool> Add(string connectionId, Guid userId);
        Task<bool> Delete(string connectionId);
        Task<List<string>> Get(Guid userId);
    }
}