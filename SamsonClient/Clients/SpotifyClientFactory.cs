using SamsonClient.Clients.Interfaces;
using SamsonClient.Execute.Spotfiy.Interfaces;
using SamsonClient.Models.Spotify;
using SamsonClient.Providers;

namespace SamsonClient.Clients
{
    public class SpotifyClientFactory(
        ISpotifyAuthProvider spotifyAuthProvider,
        ISpotifyAuthorisation spotifyAuthorisation)
        : ISpotifyClientFactory
    {
        public async Task<HttpClient> CreateSpotifyClient()
        {
            var spotifyClient = new HttpClient();
            SpotifyUserAuth spotifyUserAuth;

            try
            {
                spotifyUserAuth = await spotifyAuthProvider.GetSpotifyAccessToken();
            } catch
            {
                await spotifyAuthorisation.Authorize();
                spotifyUserAuth = await spotifyAuthProvider.GetSpotifyAccessToken();
            }

            if (spotifyUserAuth.Expires_at == DateTimeOffset.UtcNow)
            {
                spotifyAuthorisation.RefreshToken(spotifyUserAuth);
                spotifyUserAuth = await spotifyAuthProvider.GetSpotifyAccessToken();
            }

            spotifyClient.DefaultRequestHeaders.Add("Authorization", spotifyUserAuth.Token_type + " " + spotifyUserAuth.Access_token);
            return spotifyClient;
        }
    }
}
