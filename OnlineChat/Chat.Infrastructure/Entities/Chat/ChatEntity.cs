using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Entites.Chat
{
    public class ChatEntity
    {
        public int id { get; set; }
        public Guid chat_id { get; set; }
        public Guid user_id { get; set; }
    }
}
