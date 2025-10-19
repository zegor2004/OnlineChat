using Chat.Domain.Models.Chat;
namespace Chat.Domain.Abstractions.Chat
{
    public interface IChatServices
    {
        //Task<List<Chat>> GetChatsPreview(string email);
        Task<Guid> GetChat(string email_1, string email_2);
    }
}