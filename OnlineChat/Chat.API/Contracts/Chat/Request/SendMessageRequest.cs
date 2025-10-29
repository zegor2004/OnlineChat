namespace Chat.API.Contracts.Chat
{
    public record SendMessageRequest
    (
        Guid chatId,
        string text
    );
}
