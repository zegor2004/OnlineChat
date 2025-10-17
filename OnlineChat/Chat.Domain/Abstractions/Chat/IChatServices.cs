
namespace Chat.Domain.Abstractions.Chat
{
    public interface IChatServices
    {
        Task<Guid> Create(string email_1, string email_2);
    }
}