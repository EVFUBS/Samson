using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsonConsoleApp.Models
{
    public class SpotifyUserAuthRequest
    {
        public string Uri {  get; set; }
        public SpotifyUserAuthForm Form { get; set; }
        public SpotifyUserAuthHeaders Headers { get; set; }
    }

    public class SpotifyUserAuthForm
    {
        public string Code { get; set; }
        public string RedirectUri { get; set; }
        public string GrantType { get; set; }
    }

    public class SpotifyUserAuthHeaders
    {
        public string Authorization { get; set; }
        public string ContentType { get; set; }
    }
}
