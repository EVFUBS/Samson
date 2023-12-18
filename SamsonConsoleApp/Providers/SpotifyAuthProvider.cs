using SamsonConsoleApp.DAL.interfaces;
using SamsonConsoleApp.Models.Spotify;

namespace SamsonConsoleApp.Providers
{
    public class SpotifyAuthProvider : ISpotifyAuthProvider
    {
        private readonly ISpotifyDAL _spotifyDAL;

        public SpotifyAuthProvider(ISpotifyDAL spotifyDAL)
        {
            _spotifyDAL = spotifyDAL;
        }

        public void RemoveSpotifyAccessToken(SpotifyUserAuth spotifyUserAuth)
        {
            _spotifyDAL.RemoveAccessToken(spotifyUserAuth);
        }

        public SpotifyUserAuth AddSpotifyAccessToken(SpotifyUserAuth spotifyUserAuth)
        {
            spotifyUserAuth.Expires_at = DateTimeOffset.UtcNow.AddSeconds(spotifyUserAuth.Expires_in);
            var addedSpotifyAuth = _spotifyDAL.AddAccessToken(spotifyUserAuth);
            return addedSpotifyAuth;
        }

        public async Task<SpotifyUserAuth> GetSpotifyAccessToken()
        {
            var retrievedSpotifyAuthToken = await _spotifyDAL.GetAccessToken();
            return retrievedSpotifyAuthToken;
        }
    }
}
