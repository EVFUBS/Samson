namespace SamsonClient.Execute.Spotfiy.Interfaces
{
    public interface ISpotifyPlayer
    {
        Task<string> AvailableDevices();
        void PausePlayback(string? deviceId = null);
        void PlayOrResumePlayback(string? deviceId = null, Uri? songContext = null, Uri[]? songsToPlay = null, string? position = null);
        void PlayOrResumePlayback();
    }
}