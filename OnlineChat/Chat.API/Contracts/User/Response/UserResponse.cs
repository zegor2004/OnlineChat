namespace Chat.API.Contracts.User.Response
{
    public record UserResponse(
        Guid UserId,
        bool IsOnline,
        string Name);

}
