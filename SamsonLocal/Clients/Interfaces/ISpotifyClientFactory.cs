namespace SamsonLocal.Clients.Interfaces
{
    public interface ISpotifyClientFactory
    {
        Task<HttpClient> CreateSpotifyClient();
    }
}