using Chat.Infrastructure.Entites.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography.X509Certificates;

namespace Chat.Infrastructure.Configurations.User
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.email)
                .IsRequired();

            builder.Property(x => x.password_hash)
                .IsRequired();

            builder.Property(x => x.name)
                .IsRequired();

            builder.Property(x => x.created_at)
                .IsRequired();
        }
    }
}
