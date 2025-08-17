using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Models
{
    public class ChatUser
    {
        private ChatUser(string email, string password, string name)
        {
            Email = email;
            Password = password;
            Name = name;
        }
        public int Id { get; }
        public string Email { get; } = string.Empty;
        public string Password { get; } = string.Empty;
        public string Name { get; } = string.Empty;


        public static (string Mes, ChatUser User) Create(string email, string password, string name)
        {
            string mes = string.Empty;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(name))
            {

            }
            ChatUser user = new ChatUser(email, password, name);
            return (mes, user);
        }
    }
}
