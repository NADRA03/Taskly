using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;
using System;

namespace Taskly.Api.Models
{
    [Table("auth.users")]
    public class User : BaseModel
    {
        [PrimaryKey("id")]
        public Guid Id { get; set; }  // UID

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("phone")]
        public string? Phone { get; set; }

        [Column("aud")]
        public string Aud { get; set; } = string.Empty;

        [Column("role")]
        public string Role { get; set; } = string.Empty;

        [Column("user_metadata")]
        public object? UserMetadata { get; set; } 

        [Column("app_metadata")]
        public object? AppMetadata { get; set; } 

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("last_sign_in_at")]
        public DateTime? LastSignInAt { get; set; }
    }
}

