using SamsonConsoleApp.Models;

namespace SamsonConsoleApp.Actions.Spotfiy.Interfaces
{
    public interface ISpotifyAuthorisation
    {
        Task Authorize();
        void Login(SpotifyUserAuthRequest request);
    }
}