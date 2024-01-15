namespace SamsonConsoleApp.Options
{
    public class SpotifyIntegrationOptions : ISpotifyIntegrationOptions
    {
        public string SpotifyClientId { get; set; }
        public string SpotifyClientSecret { get; set; }
        public string ResponseType { get; set; }
        public string RedirectUri { get; set; }
        public string Scope { get; set; }
    }
}
