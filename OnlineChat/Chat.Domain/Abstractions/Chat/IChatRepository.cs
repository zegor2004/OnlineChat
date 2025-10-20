using Chat.Domain.Models.Chat;

namespace Chat.Domain.Abstractions.Chat
{
    public interface IChatRepository
    {
        Task<Guid> Create(string email_1, string email_2);
        Task<List<ChatModel>> GetChats(string email);
        Task<Guid> GetChat(string email_1, string email_2);
    }
}