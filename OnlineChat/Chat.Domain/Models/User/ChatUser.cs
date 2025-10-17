using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chat.Domain.Models.User
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

        static readonly private int maxLenghtEmail = 25;
        static readonly private int maxLenghtPassword = 25;
        static readonly private int maxLenghtName = 15;
        static readonly private int minLenghtPassword = 5;
        static readonly private int minLenghtName = 5;
        public static ChatUser Create(string email, string password, string name)
        {
            ChatUser user = new ChatUser(email, password, name);
            return user;
        }

        public static string DataValidation(string email, string password, string name)
        {
            string mes = string.Empty;
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(name))
            {
                mes = "All fields must be filled in";
            }
            else if (email.Count() > maxLenghtEmail
                || password.Count() < minLenghtPassword || password.Count() > maxLenghtPassword
                || name.Count() < minLenghtName || name.Count() > maxLenghtName)
            {
                mes = "The data is too small or too big";
            }
            else if (!Regex.IsMatch(email, emailPattern))
            {
                mes = "The email address is incorrect";
            }
            return mes;
        }
    }
}
