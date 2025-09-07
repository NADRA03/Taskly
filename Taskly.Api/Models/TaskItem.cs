using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;
using System;

namespace Taskly.Api.Models
{
    [Table("tasks")]
    public class TaskItem : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Column("due")]
        public DateTime? Due { get; set; }

        [Column("priority")]
        public string Priority { get; set; } = "Medium";

        [Column("status")]
        public string Status { get; set; } = "Todo";

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}

