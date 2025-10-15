using Chat.Domain.Models;

namespace Chat.Domain.Abstractions
{
    public interface IUserRepository
    {
        Task<string> Create(string email, string passwordHash, string name);
        Task<string> GetByEmail(string email);
        Task<string> Delete(string email);
        Task<List<ChatUser>> Get();
        Task<string> Update(string email, string passwordHash, string name);
    }
}