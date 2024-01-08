using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsonConsoleApp.Options
{
    public class SpotifyIntegrationOptions
    {
        public string SpotifyClientId { get; set; }
        public string SpotifyClientSecret { get; set; }
        public string ResponseType { get; set; }
        public string RedirectUri { get; set; }
        public string Scope { get; set; }
    }
}
