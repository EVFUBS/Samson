namespace SamsonConsoleApp.Options
{
    public interface ISpotifyIntegrationOptions
    {
        string RedirectUri { get; set; }
        string ResponseType { get; set; }
        string Scope { get; set; }
        string SpotifyClientId { get; set; }
        string SpotifyClientSecret { get; set; }
    }
}