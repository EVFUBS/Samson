using Microsoft.Extensions.Configuration;
using SamsonConsoleApp.Actions;
using SamsonConsoleApp.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsonConsoleApp.Models
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
            var SpotifyConfig = config.GetSection(nameof(SpotifyIntegration)).Get<SpotifyIntegrationOptions>();

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
