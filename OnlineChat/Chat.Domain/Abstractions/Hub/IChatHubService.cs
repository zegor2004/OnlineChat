namespace Chat.Domain.Abstractions.Hub
{
    public interface IChatHubService
    {
        Task NotificationNewMessage(string message, Guid userId);
    }
}