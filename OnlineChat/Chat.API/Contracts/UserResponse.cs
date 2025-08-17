namespace Chat.API.Contracts
{
    public record UserResponse(
        string Email, 
        string Password,
        string Name);

}
