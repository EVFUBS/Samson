using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsonConsoleApp.Actions.Spotfiy.Constants
{
    public class SpotifyConstants
    {
        // Use: PlayerEndpoint + Route
        public const string BaseUrl = "https://api.spotify.com/v1";
        public const string PlayerEndpoint = "/me/player";
        public const string PlayRoute = "/play";
        public const string PauseRoute = "/pause";
        public const string DevicesRoute = "/devices";
    }
}
