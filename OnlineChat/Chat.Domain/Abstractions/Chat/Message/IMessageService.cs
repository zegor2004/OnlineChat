using Chat.Domain.Models.Chat.Message;

namespace Chat.Domain.Abstractions.Chat.Message
{
    public interface IMessageService
    {
        Task<List<MessageModel>> GetMessages(Guid chatId);
        Task<MessageModel> GetLastMessage(Guid chatId);
        Task<MessageModel> SendMessage(Guid chatId, Guid userId, string text);
        Task<bool> UpdateMessageStatus(Guid messageId);
        Task<MessageModel> GetMessage(Guid messageId);
    }
}