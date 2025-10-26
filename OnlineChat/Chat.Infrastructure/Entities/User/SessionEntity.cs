using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Entities.User
{
    public class SessionEntity
    {
        public string connection_id { get; set; } = string.Empty;
        public Guid user_id { get; set; }
    }
}
