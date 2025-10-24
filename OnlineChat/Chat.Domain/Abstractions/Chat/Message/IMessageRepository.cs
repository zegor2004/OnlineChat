using Chat.Domain.Models.Chat;

namespace Chat.Domain.Abstractions.Chat.Message
{
    public interface IMessageRepository
    {
        Task<MessageModel> GetMessageLast(Guid chatId);
        Task<List<MessageModel>> Get(Guid chatId);
        Task<bool> Add(Guid chatId, Guid userId, string text, DateTime createdAt);
    }
}