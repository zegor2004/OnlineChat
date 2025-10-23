namespace Chat.API.Contracts.Chat
{
    public record SendMessageRequest
    (
        Guid userId,
        string text
    );
}
