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

        private const int MaxLenghtEmail = 25;
        private const int MaxLenghtPassword = 25;
        private const int MaxLenghtName = 15;
        private const int MinLenghtPassword = 5;
        private const int MinLenghtName = 5;
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
            else if (email.Count() > MaxLenghtEmail
                || password.Count() < MinLenghtPassword || password.Count() > MaxLenghtPassword
                || name.Count() < MinLenghtName || name.Count() > MaxLenghtName)
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
