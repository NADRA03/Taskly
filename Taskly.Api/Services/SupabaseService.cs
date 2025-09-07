using Microsoft.Extensions.Configuration;
using Supabase;

namespace Taskly.Api.Services
{
    public class SupabaseService
    {
        public Client Client { get; }

        public SupabaseService(IConfiguration config)
        {
            var url = config["Supabase:Url"] ?? throw new ArgumentNullException("Supabase:Url");
            var key = config["Supabase:Key"] ?? throw new ArgumentNullException("Supabase:Key");

            Client = new Client(url, key, new SupabaseOptions
            {
                AutoRefreshToken = true,
                AutoConnectRealtime = true
            });
        }
    }
}
