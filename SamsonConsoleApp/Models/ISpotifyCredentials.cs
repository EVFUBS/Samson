namespace SamsonConsoleApp.Models
{
    public interface ISpotifyCredentials
    {
        string RedirectUri { get; }
        string ResponseType { get; }
        string Scope { get; }
        string SpotifyClientId { get; }
        string SpotifyClientSecret { get; }
    }
}