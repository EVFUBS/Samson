using SamsonClient.Models.Spotify;

namespace SamsonClient.Execute.Spotfiy.Interfaces
{
    public interface ISpotifyAuthorisation
    {
        Task Authorize();
        void Login(SpotifyUserAuthRequest request);
        void RefreshToken(SpotifyUserAuth spotifyUserAuth);
    }
}