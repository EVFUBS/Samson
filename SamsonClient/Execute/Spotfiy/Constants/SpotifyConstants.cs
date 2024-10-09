namespace SamsonClient.Execute.Spotfiy.Constants
{
    public abstract class SpotifyConstants
    {
        // Use: PlayerEndpoint + Route
        public const string BaseUrl = "https://api.spotify.com/v1";
        public const string PlayerEndpoint = "/me/player";
        public const string PlayRoute = "/play";
        public const string PauseRoute = "/pause";
        public const string DevicesRoute = "/devices";
    }
}
