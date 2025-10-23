using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Models.User
{
    public class UserViewModel
    {
        private UserViewModel(Guid userId, string name)
        {
            UserId = userId;
            Name = name;
        }
        public Guid UserId { get; }
        public string Name { get; } = string.Empty;

        public static UserViewModel Create(Guid userId, string name)
        {
            UserViewModel user = new UserViewModel(userId, name);
            return user;
        }
    }
}
