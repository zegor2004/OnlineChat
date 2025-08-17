namespace Chat.API.Contracts
{
    public record UserRequest(
        string Email,
        string Password,
        string Name);

}
