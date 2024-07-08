using SamsonLocal.Models.Spotify;

namespace SamsonLocal.DAL.interfaces
{
    public interface ISpotifyDal
    {
        SpotifyUserAuth AddAccessToken(SpotifyUserAuth spotifyUserAuth);
        void RemoveAccessToken(SpotifyUserAuth spotifyUserAuth);
        Task<SpotifyUserAuth> GetAccessToken();
        SpotifyUserAuth UpdateAccessToken(SpotifyUserAuth spotifyUserAuth);
    }
}