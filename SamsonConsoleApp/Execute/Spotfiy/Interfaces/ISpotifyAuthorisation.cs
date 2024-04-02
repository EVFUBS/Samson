using SamsonConsoleApp.Models.Spotify;

namespace SamsonConsoleApp.Execute.Spotfiy.Interfaces
{
    public interface ISpotifyAuthorisation
    {
        Task Authorize();
        void Login(SpotifyUserAuthRequest request);
        void RefreshToken(SpotifyUserAuth spotifyUserAuth);
    }
}