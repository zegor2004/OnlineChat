using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Infrastructure.Entities.User.Session;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace Chat.Infrastructure.Configurations.User.Session
{
    public class SessionConfiguration : IEntityTypeConfiguration<SessionEntity>
    {
        public void Configure(EntityTypeBuilder<SessionEntity> builder)
        {
            builder.HasKey(x => x.connection_id);
            builder.Property(x => x.connection_id)
                .IsRequired();

            builder.Property(x => x.user_id)
                .IsRequired();

            builder.Property(x => x.created_at)
                .IsRequired();
        }
    }
}
