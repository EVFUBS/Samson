namespace SamsonConsoleApp.Clients.Interfaces
{
    public interface ISpotifyClientFactory
    {
        HttpClient CreateSpotifyClient();
        HttpClient setDefaultHeaderAuth(HttpClient spotifyClient);
    }
}