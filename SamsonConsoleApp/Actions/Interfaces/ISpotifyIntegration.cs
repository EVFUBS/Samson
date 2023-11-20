using SamsonConsoleApp.Models;

namespace SamsonConsoleApp.Actions.Interfaces
{
    public interface ISpotifyIntegration
    {
        Task Authorize();
        void Login(SpotifyUserAuthRequest request);
    }
}