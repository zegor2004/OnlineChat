using Chat.Domain.Models.Chat;

namespace Chat.Domain.Abstractions.Chat
{
    public interface IMessageRepository
    {
        Task<MessageModel> GetMessageLast(Guid chatId);
        Task<List<MessageModel>> Get(Guid chatId);
    }
}