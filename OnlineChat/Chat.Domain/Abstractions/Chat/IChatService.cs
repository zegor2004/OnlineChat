using Chat.Domain.Models.Chat;
namespace Chat.Domain.Abstractions.Chat
{
    public interface IChatService
    {
        Task<List<ChatViewModel>> GetChatPreview(Guid userId);
        Task<ChatViewModel> GetChat(Guid userIdFrom, Guid UserIdTo);
        Task<MessageModel> SendMessage(Guid userIdFrom, Guid UserIdTo, string text);
    }
}