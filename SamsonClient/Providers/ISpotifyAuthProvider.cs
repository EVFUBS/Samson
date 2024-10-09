using SamsonClient.Models.Spotify;

namespace SamsonClient.Providers
{
    public interface ISpotifyAuthProvider
    {
        SpotifyUserAuth AddSpotifyAccessToken(SpotifyUserAuth spotifyUserAuth);
        Task<SpotifyUserAuth> GetSpotifyAccessToken();
        void RemoveSpotifyAccessToken(SpotifyUserAuth spotifyUserAuth);
        SpotifyUserAuth UpdateSpotifyAccessToken(SpotifyUserAuth spotifyUserAuth);
    }
}