using Chat.Domain.Models.User;

namespace Chat.Domain.Abstractions.User
{
    public interface IUserRepository
    {
        Task<string> Create(string email, string passwordHash, string name);
        Task<string> GetByEmail(string email);
        Task<string> Delete(string email);
        Task<List<UserModel>> Get(string name);
        Task<string> Update(string email, string passwordHash, string name);
    }
}