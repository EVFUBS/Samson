using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SamsonConsoleApp.Clients.Interfaces;

namespace SamsonConsoleApp.Client
{
    public class SpotifyClientFactory : ISpotifyClientFactory
    {
        public SpotifyClientFactory() { }

        public HttpClient CreateSpotifyClient()
        {
            // update once this gets going
            return new HttpClient();
        }
    }
}
