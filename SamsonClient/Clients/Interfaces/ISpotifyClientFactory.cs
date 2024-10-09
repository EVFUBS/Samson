namespace SamsonClient.Clients.Interfaces
{
    public interface ISpotifyClientFactory
    {
        Task<HttpClient> CreateSpotifyClient();
    }
}