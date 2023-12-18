using SamsonConsoleApp.Models.Spotify;

namespace SamsonConsoleApp.DAL.interfaces
{
    public interface ISpotifyDAL
    {
        SpotifyUserAuth AddAccessToken(SpotifyUserAuth spotifyUserAuth);
        void RemoveAccessToken(SpotifyUserAuth spotifyUserAuth);
        Task<SpotifyUserAuth> GetAccessToken();
        SpotifyUserAuth UpdateAccessToken(SpotifyUserAuth spotifyUserAuth);
    }
}