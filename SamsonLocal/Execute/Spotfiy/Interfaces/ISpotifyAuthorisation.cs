using SamsonLocal.Models.Spotify;

namespace SamsonLocal.Execute.Spotfiy.Interfaces
{
    public interface ISpotifyAuthorisation
    {
        Task Authorize();
        void Login(SpotifyUserAuthRequest request);
        void RefreshToken(SpotifyUserAuth spotifyUserAuth);
    }
}