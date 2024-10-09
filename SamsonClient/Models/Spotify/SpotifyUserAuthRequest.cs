namespace SamsonClient.Models.Spotify
{
    public record SpotifyUserAuthRequest
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

        public Dictionary<string, string> AsDictionary()
        {
            return new Dictionary<string, string> {
                { "grant_type", GrantType},
                { "code", Code},
                { "redirect_uri", RedirectUri}
            };
        }
    }

    public record SpotifyUserAuthHeaders
    {
        public string Authorization { get; set; }
        public string ContentType { get; set; }
    }
}
