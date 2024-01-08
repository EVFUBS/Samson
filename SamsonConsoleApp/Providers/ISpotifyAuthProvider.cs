using SamsonConsoleApp.Models.Spotify;

namespace SamsonConsoleApp.Providers
{
    public interface ISpotifyAuthProvider
    {
        SpotifyUserAuth AddSpotifyAccessToken(SpotifyUserAuth spotifyUserAuth);
        Task<SpotifyUserAuth> GetSpotifyAccessToken();
        void RemoveSpotifyAccessToken(SpotifyUserAuth spotifyUserAuth);
    }
}