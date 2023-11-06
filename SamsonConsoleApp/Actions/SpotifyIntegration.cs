using Microsoft.Extensions.Configuration;
using SamsonConsoleApp.Actions.Interfaces;
using SamsonConsoleApp.Clients.Interfaces;
using SamsonConsoleApp.Models;
using SamsonConsoleApp.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamsonConsoleApp.Actions
{
    public class SpotifyIntegration : ISpotifyIntegration
    {
        private readonly ISpotifyClientFactory _spotifyClientFactory;
        private readonly SpotifyIntegrationOptions _spotifyOptions;

        public SpotifyIntegration(
            ISpotifyClientFactory spotifyClientFactory,
            IConfiguration config
            )
        {
            _spotifyClientFactory = spotifyClientFactory;
            var SpotifyConfig = config.GetSection(nameof(SpotifyIntegration)).Get<SpotifyIntegrationOptions>();

            if ( SpotifyConfig != null )
            {
                _spotifyOptions = SpotifyConfig;
            }
            throw new Exception("Could not get Spotify Config");
        }

        public async Task Login()
        {
            // Spotify recommneds this
            var state = GenerateState(16);
            var client = _spotifyClientFactory.CreateSpotifyClient();

            var queryParameters = new Dictionary<string, string> {
                { "response_type", _spotifyOptions.ResponseType },
                { "client_id", _spotifyOptions.SpotifyClientId },
                { "scope", _spotifyOptions.Scope },
                { "redirect_uri", _spotifyOptions.RedirectUri },
                { "state", state },
            };

            var queryString = buildQueryStringAsync(queryParameters);
            var response = await client.GetAsync($"/https://accounts.spotify.com/authorize?{queryString}");
            response.EnsureSuccessStatusCode();
        }

        public static string GenerateState(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async Task<string> buildQueryStringAsync(Dictionary<string, string> parameters)
        {
            var dictFormUrlEncoded = new FormUrlEncodedContent(parameters);
            var queryString = await dictFormUrlEncoded.ReadAsStringAsync();
            return queryString;
        }
    }
}
