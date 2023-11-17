using SamsonConsoleApp.Models.Spotify;

namespace SamsonConsoleApp.Actions.Interfaces
{
    public interface ISpotifyIntegration
    {
        Task AuthoriseSpotify();
        Task Login(SpotifyUserAuthRequest spotifyUserAuthRequest);
    }
}