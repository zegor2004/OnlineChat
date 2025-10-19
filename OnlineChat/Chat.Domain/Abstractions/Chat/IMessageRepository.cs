using Chat.Domain.Models.Chat;

namespace Chat.Domain.Abstractions.Chat
{
    public interface IMessageRepository
    {
        Task<MessageModel> GetMessageLast(string chatId);
        Task<List<MessageModel>> Get(string chatId);
    }
}