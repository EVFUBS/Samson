using Microsoft.Extensions.Configuration;

namespace SamsonClient.Options
{
    public class SpotifyIntegrationOptions : ISpotifyIntegrationOptions
    {
        public SpotifyIntegrationOptions(IConfiguration config)
        {
            SpotifyClientId = config.GetSection("SpotifyIntegrationOptions").GetValue<string>("SpotifyClientId");
            SpotifyClientSecret = config.GetSection("SpotifyIntegrationOptions").GetValue<string>("SpotifyClientSecret");
            ResponseType = config.GetSection("SpotifyIntegrationOptions").GetValue<string>("ResponseType");
            RedirectUri = config.GetSection("SpotifyIntegrationOptions").GetValue<string>("RedirectUri");
            Scope = config.GetSection("SpotifyIntegrationOptions").GetValue<string>("Scope");
        }

        public string SpotifyClientId { get; set; }
        public string SpotifyClientSecret { get; set; }
        public string ResponseType { get; set; }
        public string RedirectUri { get; set; }
        public string Scope { get; set; }
    }
}
