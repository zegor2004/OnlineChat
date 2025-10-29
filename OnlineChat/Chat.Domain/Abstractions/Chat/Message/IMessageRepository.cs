using Chat.Domain.Models.Chat.Message;

namespace Chat.Domain.Abstractions.Chat.Message
{
    public interface IMessageRepository
    {
        Task<MessageModel> GetLastMessage(Guid chatId);
        Task<List<MessageModel>> GetMessages(Guid chatId);
        Task<bool> AddMessage(MessageModel message);
        Task<bool> UpdateMessageStatus(Guid messageId);
        Task<MessageModel> GetMessage(Guid messageId);
    }
}