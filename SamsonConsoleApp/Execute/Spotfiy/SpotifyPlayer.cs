using SamsonConsoleApp.Execute.Spotfiy.Constants;
using SamsonConsoleApp.Execute.Spotfiy.Interfaces;
using SamsonConsoleApp.Clients.Interfaces;

namespace SamsonConsoleApp.Execute.Spotfiy
{
    public class SpotifyPlayer : ISpotifyPlayer
    {

        private readonly ISpotifyClientFactory _spotifyClientFactory;

        public SpotifyPlayer(
            ISpotifyClientFactory spotifyClientFactory
            )
        {
            _spotifyClientFactory = spotifyClientFactory;
        }

        public async Task<string> AvailableDevices()
        {
            var spotifyClient = await _spotifyClientFactory.CreateSpotifyClient();
            var response = await spotifyClient.GetAsync(SpotifyConstants.BaseUrl + SpotifyConstants.PlayerEndpoint + SpotifyConstants.DevicesRoute);
            var devices = await response.Content.ReadAsAsync<string>();
            return devices;
        }

        public async void PausePlayback(string? deviceId = null)
        {
            var spotifyClient = await _spotifyClientFactory.CreateSpotifyClient();
            await spotifyClient.PutAsync(SpotifyConstants.BaseUrl + SpotifyConstants.PlayerEndpoint + SpotifyConstants.PauseRoute, null);
        }

        public async void PlayOrResumePlayback()
        {
            var spotifyClient = await _spotifyClientFactory.CreateSpotifyClient();
            await spotifyClient.PutAsync(SpotifyConstants.BaseUrl + SpotifyConstants.PlayerEndpoint + SpotifyConstants.PlayRoute, null);
        }

        public async void PlayOrResumePlayback(string? deviceId = null, Uri? songContext = null, Uri[]? songsToPlay = null, string? position = null)
        {
            var spotifyClient = await _spotifyClientFactory.CreateSpotifyClient();

            var request = new Dictionary<string, string>
            {
                { "context_uri", songContext.OriginalString },
                { "offset", new Dictionary<string, string> { { "position", position } }.ToString() },
                { "position_ms", "0" }
            };

            var httpContent = new StringContent(request.ToString());
            await spotifyClient.PutAsync(SpotifyConstants.BaseUrl + SpotifyConstants.PlayerEndpoint + SpotifyConstants.PlayRoute, httpContent);
        }
    }
}
