using Chat.Domain.Models.User;

namespace Chat.Domain.Abstractions.User
{
    public interface IUserService
    {
        Task<string> Registration(string email, string password, string name);
        Task<string> Login(string email, string password);
        Task<string> DeleteUser(string email);
        Task<List<ChatUser>> FindUser(string name);
        Task<string> UpdateUser(string email, string password, string name);
    }
}