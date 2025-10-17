namespace Chat.Domain.Abstractions.Chat
{
    public interface IChatRepository
    {
        Task<Guid> Create(string email_1, string email_2);
        Task<Guid> Get(string email_1, string email_2);
    }
}