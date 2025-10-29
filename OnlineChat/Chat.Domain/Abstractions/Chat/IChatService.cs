using Chat.Domain.Models.Chat;
using Chat.Domain.Models.Chat.Message;
namespace Chat.Domain.Abstractions.Chat
{
    public interface IChatService
    {
        Task<List<ChatViewModel>> GetChatsPreview(Guid userId);
        Task<ChatViewModel> GetChat(Guid chatId, Guid userIdFrom);
        Task<Guid> GetChatId(Guid userIdFrom, Guid userIdTo);
        Task<MessageModel> SendMessage(Guid userIdFrom, Guid chatId, string text);
        Task<bool> UpdateMessageStatus(Guid messageId);
    }
}