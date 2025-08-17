using Chat.Domain.Models;

namespace Chat.Application.Services
{
    public interface IUserService
    {
        Task<string> CreateUser(ChatUser user);
        Task<string> DeleteUser(string email);
        Task<List<ChatUser>> GetAllUsers();
        Task<string> UpdateUser(string email, string password, string name);
    }
}