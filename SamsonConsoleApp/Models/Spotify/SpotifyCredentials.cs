using Microsoft.Extensions.Configuration;
using SamsonConsoleApp.Models.Spotify.Interfaces;
using SamsonConsoleApp.Options;

namespace SamsonConsoleApp.Models.Spotify
{
    public class SpotifyCredentials : ISpotifyCredentials
    {
        private readonly SpotifyIntegrationOptions _spotifyOptions;

        public string SpotifyClientId { get; }
        public string SpotifyClientSecret { get; }
        public string ResponseType { get; }
        public string RedirectUri { get; }
        public string Scope { get; }

        public SpotifyCredentials(IConfiguration config)
        {
            var SpotifyConfig = config.GetSection("SpotifyIntegration").Get<SpotifyIntegrationOptions>();

            if (SpotifyConfig != null)
            {
                _spotifyOptions = SpotifyConfig;
            }
            else
            {
                throw new Exception("Could not get Spotify Config");
            }

            SpotifyClientId = _spotifyOptions.SpotifyClientId;
            SpotifyClientSecret = _spotifyOptions.SpotifyClientSecret;
            ResponseType = _spotifyOptions.ResponseType;
            RedirectUri = _spotifyOptions.RedirectUri;
            Scope = _spotifyOptions.Scope;
        }
    }
}
