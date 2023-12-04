using SamsonConsoleApp.Actions.Interfaces;
using SamsonConsoleApp.Actions.Spotfiy.Constants;
using SamsonConsoleApp.Actions.Spotfiy.Interfaces;
using SamsonConsoleApp.Clients.Interfaces;
using SamsonConsoleApp.DAL.interfaces;
using SamsonConsoleApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SamsonConsoleApp.Actions.Spotfiy
{
    public class SpotifyPlayer : ISpotifyPlayer
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
            _spotifyClient = _spotifyClientFactory.setDefaultHeaderAuth(_spotifyClientFactory.CreateSpotifyClient());
        }


        public async Task<string> AvailableDevices()
        {
            var response = await _spotifyClient.GetAsync(SpotifyConstants.BaseUrl + SpotifyConstants.PlayerEndpoint + SpotifyConstants.DevicesRoute);
            var devices = await response.Content.ReadAsAsync<string>();
            return devices;
        }

        public async void PausePlayback(string? deviceId = null)
        {
            await _spotifyClient.PutAsync(SpotifyConstants.BaseUrl + SpotifyConstants.PlayerEndpoint + SpotifyConstants.PauseRoute, null);
        }

        public async void PlayOrResumePlayback()
        {
            await _spotifyClient.PutAsync(SpotifyConstants.BaseUrl + SpotifyConstants.PlayerEndpoint + SpotifyConstants.PlayRoute, null);
        }

        public async void PlayOrResumePlayback(string? deviceId = null, Uri? songContext = null, Uri[]? songsToPlay = null, string? position = null)
        {

            var request = new Dictionary<string, string>
            {
                { "context_uri", songContext.OriginalString },
                { "offset", new Dictionary<string, string> { { "position", position } }.ToString() },
                { "position_ms", "0" }
            };

            var httpContent = new StringContent(request.ToString());
            await _spotifyClient.PutAsync(SpotifyConstants.BaseUrl + SpotifyConstants.PlayerEndpoint + SpotifyConstants.PlayRoute, httpContent);
        }

        public async void TransferDevices()
        {
        }
    }
}
