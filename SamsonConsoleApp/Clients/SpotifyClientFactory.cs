using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SamsonConsoleApp.Clients.Interfaces;
using SamsonConsoleApp.DAL.interfaces;
using SamsonConsoleApp.Providers;

namespace SamsonConsoleApp.Client
{
    public class SpotifyClientFactory : ISpotifyClientFactory
    {
        private readonly ISpotifyAuthProvider _spotifyAuthProvider;

        public SpotifyClientFactory(ISpotifyAuthProvider spotifyAuthProvider) {
            _spotifyAuthProvider = spotifyAuthProvider;
        }

        public HttpClient CreateSpotifyClient()
        {
            var spotifyClient = new HttpClient();
            return spotifyClient;
        }

        public HttpClient setDefaultHeaderAuth(HttpClient spotifyClient)
        {
            var spotifyAuth = _spotifyAuthProvider.GetSpotifyAccessToken().Result;
            spotifyClient.DefaultRequestHeaders.Add("Authorization", spotifyAuth.Token_type + " " + spotifyAuth.Access_token);
            return spotifyClient;
        }
    }
}
