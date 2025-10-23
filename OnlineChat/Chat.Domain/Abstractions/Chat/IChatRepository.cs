using Chat.Domain.Models.Chat;

namespace Chat.Domain.Abstractions.Chat
{
    public interface IChatRepository
    {
        Task<Guid> Create(Guid userIdFrom, Guid userIdTo);
        Task<List<ChatModel>> GetChats(Guid userId);
        Task<Guid> GetChat(Guid userIdFrom, Guid userIdTo);
    }
}