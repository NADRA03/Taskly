using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;
using System;
using System.Collections.Generic;

namespace Taskly.Api.Models
{
    [Table("chatbot_logs")]
    public class ChatbotLog : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("message")]
        public string Message { get; set; } = string.Empty;

        [Column("reply")]
        public string Reply { get; set; } = string.Empty;

        [Column("actions")]
        public Dictionary<string, object> Actions { get; set; } = new();

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
