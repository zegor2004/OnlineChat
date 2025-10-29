using Chat.Domain.Models.Chat;

namespace Chat.Domain.Abstractions.Chat
{
    public interface IChatRepository
    {
        Task<Guid> Create(Guid userIdFrom, Guid userIdTo);
        Task<List<ChatModel>> GetChats(Guid userId);
        Task<Guid> GetChatId(Guid userIdFrom, Guid userIdTo);
        Task<Guid> GetUserIdFromChat(Guid chatId, Guid userIdFrom);
    }
}