using SamsonConsoleApp.Actions.Interfaces;
using SamsonConsoleApp.Actions.Spotfiy.Constants;
using SamsonConsoleApp.Clients.Interfaces;
using SamsonConsoleApp.DAL.interfaces;
using SamsonConsoleApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsonConsoleApp.Actions.Spotfiy
{
    public class SpotifyPlayer
    {

        private readonly ISpotifyClientFactory _spotifyClientFactory;
        private readonly ISpotifyCredentials _spotifyCredentials;
        private readonly ISpotifyDAL _spotifyDAL;
        private readonly HttpClient _spotifyClient;

        public SpotifyPlayer(
            ISpotifyClientFactory spotifyClientFactory,
            ISpotifyCredentials spotifyCredentials,
            ISpotifyDAL spotifyDAL
            )
        {
            _spotifyClientFactory = spotifyClientFactory;
            _spotifyCredentials = spotifyCredentials;
            _spotifyDAL = spotifyDAL;
            _spotifyClient = _spotifyClientFactory.CreateSpotifyClient();
        }
        public async void PausePlayback(string? deviceId)
        {
            setDefaultHeaderAuth();
            await _spotifyClient.PutAsync(_spotifyCredentials.BaseEndpoint + SpotifyConstants.PlayerEndpoint + SpotifyConstants.PauseRoute, null);
        }

        public async void PlayOrResumePlayback(string? deviceId, Uri? songContext, Uri[] songsToPlay, string position)
        {
            setDefaultHeaderAuth();

            var request = new Dictionary<string, string>
            {
                { "context_uri", songContext.OriginalString },
                { "offset", new Dictionary<string, string> { { "position", position } }.ToString() },
                { "position_ms", "0" }
            };

            var httpContent = new StringContent(request.ToString());
            await _spotifyClient.PutAsync(_spotifyCredentials.BaseEndpoint + SpotifyConstants.PlayerEndpoint + SpotifyConstants.PlayRoute, httpContent);
        }

        private async void setDefaultHeaderAuth()
        {
            var spotifyAuth = await _spotifyDAL.RetrieveAccessToken();
            _spotifyClient.DefaultRequestHeaders.Add("Authorization", spotifyAuth.token_type + " " + spotifyAuth.access_token);
        }
    }
}
