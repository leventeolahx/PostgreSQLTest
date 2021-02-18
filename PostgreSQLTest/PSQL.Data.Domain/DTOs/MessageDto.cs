using System.Collections.Generic;

using PSQL.Data.Domain.Models;

namespace PSQL.Data.Domain.DTOs
{
    public class MessageDto
    {
        public long ResponseTimeMilliseconds { get; set; }
        public List<Message> Messages {get;set;} = new List<Message>();
    }
}
