using Chat.Domain.Models;

namespace Chat.Domain.Abstractions
{
    public interface IUserService
    {
        Task<string> Registration(string email, string password, string name);
        Task<string> Login(string email, string password);
        Task<string> DeleteUser(string email);
        Task<List<ChatUser>> GetAllUsers();
        Task<string> UpdateUser(string email, string password, string name);
    }
}