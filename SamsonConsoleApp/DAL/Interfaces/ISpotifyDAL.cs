using SamsonConsoleApp.Models;

namespace SamsonConsoleApp.DAL.interfaces
{
    public interface ISpotifyDAL
    {
        void AddAccessToken(SpotifyUserAuth spotifyUserAuth);
    }
}