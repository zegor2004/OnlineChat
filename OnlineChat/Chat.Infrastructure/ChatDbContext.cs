using Chat.Infrastructure.Entites.Chat;
using Chat.Infrastructure.Entites.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
        {
        }

        protected ChatDbContext()
        {
        }

        public DbSet<UserEntity> users { get; set; } = null!;

        public DbSet<ChatEntity> chats { get; set; } = null!;
        public DbSet<MessageEntity> messages { get; set; } = null!;
    }
}
