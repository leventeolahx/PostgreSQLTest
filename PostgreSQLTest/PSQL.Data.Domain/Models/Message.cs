using NpgsqlTypes;

namespace PSQL.Data.Domain.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }        
        public NpgsqlTsVector SearchVector { get; set; }
    }
}
