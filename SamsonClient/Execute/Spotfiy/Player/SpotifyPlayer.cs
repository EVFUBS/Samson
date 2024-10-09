using SamsonClient.Clients.Interfaces;
using SamsonClient.Execute.Spotfiy.Constants;
using SamsonClient.Execute.Spotfiy.Interfaces;

namespace SamsonClient.Execute.Spotfiy.Player
{
    public class SpotifyPlayer(ISpotifyClientFactory spotifyClientFactory) : ISpotifyPlayer
    {
        public async Task<string> AvailableDevices()
        {
            var spotifyClient = await spotifyClientFactory.CreateSpotifyClient();
            var response = await spotifyClient.GetAsync(SpotifyConstants.BaseUrl + SpotifyConstants.PlayerEndpoint + SpotifyConstants.DevicesRoute);
            var devices = await response.Content.ReadAsAsync<string>();
            return devices;
        }

        public async void PausePlayback(string? deviceId = null)
        {
            var spotifyClient = await spotifyClientFactory.CreateSpotifyClient();
            await spotifyClient.PutAsync(SpotifyConstants.BaseUrl + SpotifyConstants.PlayerEndpoint + SpotifyConstants.PauseRoute, null);
        }

        public async void PlayOrResumePlayback()
        {
            var spotifyClient = await spotifyClientFactory.CreateSpotifyClient();
            await spotifyClient.PutAsync(SpotifyConstants.BaseUrl + SpotifyConstants.PlayerEndpoint + SpotifyConstants.PlayRoute, null);
        }

        public async void PlayOrResumePlayback(string? deviceId = null, Uri? songContext = null, Uri[]? songsToPlay = null, string? position = null)
        {
            var spotifyClient = await spotifyClientFactory.CreateSpotifyClient();

            var request = new Dictionary<string, string>
            {
                { "context_uri", songContext.OriginalString },
                { "offset", new Dictionary<string, string> { { "position", position } }.ToString() },
                { "position_ms", "0" }
            };

            var httpContent = new StringContent(request.ToString() ?? string.Empty);
            await spotifyClient.PutAsync(SpotifyConstants.BaseUrl + SpotifyConstants.PlayerEndpoint + SpotifyConstants.PlayRoute, httpContent);
        }
    }
}
