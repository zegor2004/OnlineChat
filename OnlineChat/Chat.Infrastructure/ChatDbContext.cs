using Chat.Infrastructure.Configurations.Chat;
using Chat.Infrastructure.Configurations.Chat.Message;
using Chat.Infrastructure.Configurations.User;
using Chat.Infrastructure.Configurations.User.Session;
using Chat.Infrastructure.Entites.Chat;
using Chat.Infrastructure.Entites.User;
using Chat.Infrastructure.Entities.Chat.Message;
using Chat.Infrastructure.Entities.User.Session;
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new SessionConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserEntity> users { get; set; } = null!;
        public DbSet<ChatEntity> chats { get; set; } = null!;
        public DbSet<MessageEntity> messages { get; set; } = null!;
        public DbSet<SessionEntity> sessions { get; set; } = null!;
    }
}
