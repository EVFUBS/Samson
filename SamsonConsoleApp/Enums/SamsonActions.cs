namespace SamsonConsoleApp.enums
{
    public enum SamsonActions
    {
        // Increment by a 1000 for each integration
        // ensure enough unique actions that will be
        // needed by any one integration.

        // General
        Greet = 0,
        Question = 1,

        // Web browsers
        WebBrowserOpenWebBrowserToUrl = 1000,
        WebBrowserOpenGoogleBrowser = 1001,

        // Spotify
        SpotifyAvailableDevices = 2000,
        SpotifyPlayOrResumePlayback = 2001,
        SpotifyPausePlayback = 2002,

        // Did not understand
        DoNotUnderstand = 100000
    }
}
