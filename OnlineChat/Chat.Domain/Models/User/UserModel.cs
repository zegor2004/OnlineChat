using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chat.Domain.Models.User
{
    public class UserModel
    {
        private UserModel(Guid userId, string email, string password, string name)
        {
            UserId = userId;
            Email = email;
            Password = password;
            Name = name;
        }
        public Guid UserId { get; }
        public string Email { get; } = string.Empty;
        public string Password { get; } = string.Empty;
        public string Name { get; } = string.Empty;

        private const int MaxLenghtEmail = 25;
        private const int MaxLenghtPassword = 25;
        private const int MaxLenghtName = 15;
        private const int MinLenghtPassword = 5;
        private const int MinLenghtName = 5;
        public static UserModel Create(Guid userId, string email, string password, string name)
        {
            UserModel user = new UserModel(userId, email, password, name);
            return user;
        }
        public static UserModel CreateEmpty()
        {
            UserModel user = new UserModel(Guid.Empty, string.Empty, string.Empty, string.Empty);
            return user;
        }

        public static string DataValidation(string email, string password, string name)
        {
            string mes = string.Empty;
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            string passwordPattern = @"^[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]*$";
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
            else if (!Regex.IsMatch(password, passwordPattern))
            {
                mes = "The password is incorrect";
            }
            return mes;
        }
    }
}
