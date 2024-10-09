namespace SamsonClient.Models.Spotify.Interfaces
{
    public interface ISpotifyUserAuth
    {
        string Access_token { get; set; }
        int Expires_in { get; set; }
        int Id { get; set; }
        string Refresh_token { get; set; }
        string Scope { get; set; }
        string Token_type { get; set; }
        DateTimeOffset Expires_at { get; set; }
    }
}