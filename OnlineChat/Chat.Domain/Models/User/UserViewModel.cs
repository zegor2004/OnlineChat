using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Models.User
{
    public class UserViewModel
    {
        private UserViewModel(Guid userId, string name, bool isOnline)
        {
            UserId = userId;
            Name = name;
            IsOnline = isOnline;
        }
        public Guid UserId { get; }
        public string Name { get; } = string.Empty;
        public bool IsOnline { get; private set; } = false;

        public static UserViewModel Create(Guid userId, string name, bool isOnline)
        {
            UserViewModel user = new UserViewModel(userId, name, isOnline);
            return user;
        }

        public void UpdateStatus(bool isOnline)
        {
            this.IsOnline = isOnline;
        }

    }
}
