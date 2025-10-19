using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Entites.Chat
{
    public class MessageEntity
    {
        public int id { get; set; }
        public string chat_id { get; set; } = string.Empty;
        public string user_id { get; set; } = string.Empty;
        public string text { get; set; } = string.Empty;
        public DateTime created_at { get; set; }

    }
}
