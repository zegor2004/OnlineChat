namespace Chat.API.Contracts.User.Request
{
    public record LoginUserRequest(
        string Email,
        string Password);

}
