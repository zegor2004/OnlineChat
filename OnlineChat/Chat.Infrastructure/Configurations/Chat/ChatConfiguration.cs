using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Infrastructure.Entites.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infrastructure.Configurations.Chat
{
    public class ChatConfiguration : IEntityTypeConfiguration<ChatEntity>
    {
        public void Configure(EntityTypeBuilder<ChatEntity> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.chat_id)
                .IsRequired();

            builder.Property(x => x.user_id)
                .IsRequired();
        }
    }
}
