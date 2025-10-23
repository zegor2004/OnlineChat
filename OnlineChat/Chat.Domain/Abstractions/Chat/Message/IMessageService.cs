using Chat.Domain.Models.Chat;

namespace Chat.Domain.Abstractions.Chat.Message
{
    public interface IMessageService
    {
        Task<List<MessageModel>> GetMessages(Guid chat_id);
        Task<MessageModel> GetMessageLast(Guid chat_id);
        Task<MessageModel> SendMessage(Guid chatId, Guid userId, string text);
    }
}