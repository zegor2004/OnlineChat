using Chat.Domain.Models.User;

namespace Chat.Domain.Abstractions.User
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserByEmail(string email);
        Task<string> Create(string email, string passwordHash, string name);
        Task<string> GetPasswordByEmail(string email);
        Task<string> Delete(string email);
        Task<List<UserModel>> GetUsersByName(string name);
        Task<string> Update(string email, string passwordHash, string name);
    }
}