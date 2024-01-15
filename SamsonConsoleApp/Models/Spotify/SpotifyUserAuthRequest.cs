namespace SamsonConsoleApp.Models.Spotify
{
    public class SpotifyUserAuthRequest
    {
        public string Uri { get; set; }
        public SpotifyUserAuthForm Form { get; set; }
        public SpotifyUserAuthHeaders Headers { get; set; }
    }

    public class SpotifyUserAuthForm
    {
        public string Code { get; set; }
        public string RedirectUri { get; set; }
        public string GrantType { get; set; }
    }

    public class SpotifyUserAuthHeaders
    {
        public string Authorization { get; set; }
        public string ContentType { get; set; }
    }
}
