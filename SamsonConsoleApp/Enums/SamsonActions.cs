using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsonConsoleApp.enums
{
    public enum SamsonActions
    {
        // general voice assistant capabilites
        Greet,
        Question,

        // web browsers
        WebBrowserOpenWebBrowser,
        WebBrowserOpenGoogleBrowser,

        // spotify
        SpotifyAvailableDevices,
        SpotifyPlayOrResumePlayback,
        SpotifyPausePlayback
    }
}
