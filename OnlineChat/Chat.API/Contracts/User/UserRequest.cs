namespace Chat.API.Contracts.User
{
    public record UserRequest(
        string Email,
        string Password,
        string Name);

}
