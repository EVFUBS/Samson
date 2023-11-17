using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsonConsoleApp.Models.Spotify
{
    public class SpotifyUserAuthRequest
    {
        public string Url { get; set; }
        public SpotifyForm form { get; set; }
        public SpotifyHeader headers { get; set; }
    }

    public class SpotifyForm
    {
        public string code { get; set; }
        public string redirect_uri { get; set; }
        public string grant_type { get; set; }
    }

    public class SpotifyHeader {
        public string content_type { get; set; }
        public string Authorization { get; set; }
    }
}
