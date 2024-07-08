enum Actions
{
    // Increment by a 1000 for each integration
    // ensure enough unique actions that will be
    // needed by any one integration.

    // General
    GeneralStart = 1000,
    Greet = 1001,
    Question = 1002,
    OpenWebBrowser = 1003,
    OpenGoogleBrowser = 1004,
    GeneralEnd = 2999,

    // Spotify
    SpotifyStart = 3000,
    SpotifyAvailableDevices = 3001,
    SpotifyPlayOrResumePlayback = 3002,
    SpotifyPausePlayback = 3003,
    SpotifyStartPlaylist = 3004,
    SpotifyEnd = 3999,

    // Did not understand
    DoNotUnderstand = 100000
}

export default Actions