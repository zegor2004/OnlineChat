using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Entities.User.Session
{
    public class SessionEntity
    {
        public string connection_id { get; set; } = string.Empty;
        public Guid user_id { get; set; }
        public DateTime created_at { get; set; }
    }
}
