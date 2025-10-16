using Chat.Infrastructure.Entites;
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

        public DbSet<UserEntity> Users { get; set; } = null!;

        public DbSet<ChatEntity> Chats { get; set; } = null!;
    }
}
