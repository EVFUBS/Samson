using SamsonClient.Models.Spotify;

namespace SamsonClient.DAL.interfaces
{
    public interface ISpotifyDAL
    {
        SpotifyUserAuth AddAccessToken(SpotifyUserAuth spotifyUserAuth);
        void RemoveAccessToken(SpotifyUserAuth spotifyUserAuth);
        Task<SpotifyUserAuth> GetAccessToken();
        SpotifyUserAuth UpdateAccessToken(SpotifyUserAuth spotifyUserAuth);
    }
}