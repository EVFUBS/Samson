namespace SamsonConsoleApp.Clients.Interfaces
{
    public interface ISpotifyClientFactory
    {
        Task<HttpClient> CreateSpotifyClient();
    }
}