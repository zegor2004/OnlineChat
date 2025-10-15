using Chat.Domain.Models;

namespace Chat.Domain.Abstractions
{
    public interface IUserService
    {
        Task<string> CreateUser(ChatUser user);
        Task<string> Login(ChatUser user);
        Task<string> DeleteUser(string email);
        Task<List<ChatUser>> GetAllUsers();
        Task<string> UpdateUser(string email, string passwordHash, string name);
    }
}