using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Entites.Chat
{
    public class ChatEntity
    {
        public int Id { get; set; }
        public Guid ChatId { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
