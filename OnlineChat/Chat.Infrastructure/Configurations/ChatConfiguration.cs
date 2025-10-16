using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Infrastructure.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infrastructure.Configurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<ChatEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Email)
                .IsRequired();

            builder.Property(x => x.PasswordHash)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired();
        }
    }
}
