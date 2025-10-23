using Chat.Domain.Models.User;

namespace Chat.Domain.Abstractions.User
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserByEmail(string email);
        Task<UserViewModel> GetUserByUserId(Guid userId);
        Task<string> Create(Guid id, string email, string passwordHash, string name);
        Task<bool> GetUserBusy(string email);
        Task<string> Delete(string email);
        Task<List<UserViewModel>> GetUsersByName(string name);
        Task<string> Update(string email, string passwordHash, string name);
    }
}