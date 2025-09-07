using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;
using System;
using System.Collections.Generic;

namespace Taskly.Api.Models
{
    [Table("user_settings")]
    public class UserSettings : BaseModel
    {
        [PrimaryKey("user_id")]
        public Guid UserId { get; set; }

        [Column("settings")]
        public Dictionary<string, object> Settings { get; set; } = new();

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}

