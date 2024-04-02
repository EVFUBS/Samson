using SamsonConsoleApp.Execute.Spotfiy.Interfaces;
using SamsonConsoleApp.Clients.Interfaces;
using SamsonConsoleApp.Models.Spotify;
using SamsonConsoleApp.Providers;

namespace SamsonConsoleApp.Client
{
    public class SpotifyClientFactory : ISpotifyClientFactory
    {
        private readonly ISpotifyAuthProvider _spotifyAuthProvider;
        private readonly ISpotifyAuthorisation _spotifyAuthorisation;

        public SpotifyClientFactory(
            ISpotifyAuthProvider spotifyAuthProvider,
            ISpotifyAuthorisation spotifyAuthorisation
            ) {
            _spotifyAuthProvider = spotifyAuthProvider;
            _spotifyAuthorisation = spotifyAuthorisation;
        }

        public async Task<HttpClient> CreateSpotifyClient()
        {
            var spotifyClient = new HttpClient();
            SpotifyUserAuth spotifyUserAuth;

            try
            {
                spotifyUserAuth = await _spotifyAuthProvider.GetSpotifyAccessToken();
            } catch
            {
                await _spotifyAuthorisation.Authorize();
                spotifyUserAuth = await _spotifyAuthProvider.GetSpotifyAccessToken();
            }

            if (spotifyUserAuth.Expires_at == DateTimeOffset.UtcNow)
            {
                _spotifyAuthorisation.RefreshToken(spotifyUserAuth);
                spotifyUserAuth = await _spotifyAuthProvider.GetSpotifyAccessToken();
            }

            spotifyClient.DefaultRequestHeaders.Add("Authorization", spotifyUserAuth.Token_type + " " + spotifyUserAuth.Access_token);
            return spotifyClient;
        }
    }
}
