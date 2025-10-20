namespace Chat.API.Contracts.Chat
{
    public record SendMessageRequest
    (
        string email,
        string text
    );
}
