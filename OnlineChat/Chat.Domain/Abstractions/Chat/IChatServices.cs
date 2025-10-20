using Chat.Domain.Models.Chat;
namespace Chat.Domain.Abstractions.Chat
{
    public interface IChatServices
    {
        Task<List<ChatViewModel>> GetChatPreview(string email);
        Task<ChatViewModel> GetChat(string email_1, string email_2);
        Task<MessageModel> SendMessage(string email_1, string email_2, string text);
    }
}