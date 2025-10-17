
namespace Chat.Domain.Abstractions.Chat
{
    public interface IChatServices
    {
        Task<Guid> SendMessage(string email_1, string email_2);
    }
}