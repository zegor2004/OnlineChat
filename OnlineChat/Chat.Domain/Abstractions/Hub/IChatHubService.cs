using Chat.Domain.Models.Chat.Message;
using Chat.Domain.Models.User;

namespace Chat.Domain.Abstractions.Hub
{
    public interface IChatHubService
    {
        Task NotificationNewMessage(MessageModel message, Guid userId);
        Task UpdateStatusMessage(MessageModel message);
    }
}