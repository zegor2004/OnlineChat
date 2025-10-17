namespace Chat.API.Contracts.User.Request
{
    public record RegUserRequest(
        string Email,
        string Password,
        string Name);

}
