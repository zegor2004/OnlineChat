using Chat.Domain.Models.Chat.Message;

namespace Chat.Domain.Abstractions.Hub
{
    public interface IChatHubService
    {
        Task NotificationNewMessage(MessageModel message, Guid userId);
        Task UpdateStatusMessage(MessageModel message);
        
    }
}