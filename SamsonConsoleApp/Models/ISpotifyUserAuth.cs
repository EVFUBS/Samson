namespace SamsonConsoleApp.Models
{
    public interface ISpotifyUserAuth
    {
        string access_token { get; set; }
        int expires_in { get; set; }
        int id { get; set; }
        string refresh_token { get; set; }
        string scope { get; set; }
        string token_type { get; set; }
    }
}