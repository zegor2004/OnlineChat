using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Entities.Chat.Message
{
    public class MessageEntity
    {
        public Guid id { get; set; }
        public Guid chat_id { get; set; }
        public Guid user_id { get; set; }
        public string text { get; set; } = string.Empty;
        public bool is_read { get; set; }
        public DateTime created_at { get; set; }

    }
}
