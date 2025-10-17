using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Entites.User
{
    public class UserEntity
    {
        public int id { get; set; }
        public string email { get; set; } = string.Empty;
        public string password_hash { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public DateTime created_at { get; set; }
    }
}
