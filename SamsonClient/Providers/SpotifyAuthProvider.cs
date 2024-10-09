using SamsonClient.DAL.interfaces;
using SamsonClient.Models.Spotify;

namespace SamsonClient.Providers
{
    public class SpotifyAuthProvider(ISpotifyDAL spotifyDal) : ISpotifyAuthProvider
    {
        public void RemoveSpotifyAccessToken(SpotifyUserAuth spotifyUserAuth)
        {
            spotifyDal.RemoveAccessToken(spotifyUserAuth);
        }

        public SpotifyUserAuth AddSpotifyAccessToken(SpotifyUserAuth spotifyUserAuth)
        {
            spotifyUserAuth.Expires_at = DateTimeOffset.UtcNow.AddSeconds(spotifyUserAuth.Expires_in);
            var addedSpotifyAuth = spotifyDal.AddAccessToken(spotifyUserAuth);
            return addedSpotifyAuth;
        }

        public SpotifyUserAuth UpdateSpotifyAccessToken(SpotifyUserAuth spotifyUserAuth)
        {
            spotifyUserAuth.Expires_at = DateTimeOffset.UtcNow.AddSeconds(spotifyUserAuth.Expires_in);
            var addedSpotifyAuth = spotifyDal.UpdateAccessToken(spotifyUserAuth);
            return addedSpotifyAuth;
        }

        public async Task<SpotifyUserAuth> GetSpotifyAccessToken()
        {
            var retrievedSpotifyAuthToken = await spotifyDal.GetAccessToken();
            return retrievedSpotifyAuthToken;
        }
    }
}
