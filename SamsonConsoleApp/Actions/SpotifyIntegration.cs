using Microsoft.Extensions.Configuration;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using SamsonConsoleApp.Actions.Interfaces;
using SamsonConsoleApp.Clients.Interfaces;
using SamsonConsoleApp.DAL.interfaces;
using SamsonConsoleApp.Models;
using SamsonConsoleApp.Models.Interfaces;
using SamsonConsoleApp.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SamsonConsoleApp.Actions
{
    public class SpotifyIntegration : ISpotifyIntegration
    {
        private readonly ISpotifyClientFactory _spotifyClientFactory;
        private readonly IWebBrowser _webBrowser;
        private readonly ISpotifyCredentials _spotifyCredentials;
        private readonly ISpotifyDAL _spotifyDAL;

        public SpotifyIntegration(
            ISpotifyClientFactory spotifyClientFactory,
            IWebBrowser webBrowser,
            ISpotifyCredentials spotifyCredentials,
            ISpotifyDAL spotifyDAL
            )
        {
            _spotifyClientFactory = spotifyClientFactory;
            _webBrowser = webBrowser;
            _spotifyCredentials = spotifyCredentials;
            _spotifyDAL = spotifyDAL;
        }

        public async Task Authorize()
        {
            // Spotify recommneds this
            var state = GenerateState(16);
            var client = _spotifyClientFactory.CreateSpotifyClient();

            var queryParameters = new Dictionary<string, string> {
                { "response_type", _spotifyCredentials.ResponseType },
                { "client_id", _spotifyCredentials.SpotifyClientId },
                { "scope", _spotifyCredentials.Scope },
                { "redirect_uri", _spotifyCredentials.RedirectUri },
                { "state", state },
            };

            var queryString = await buildQueryStringAsync(queryParameters);
            var response = await client.GetAsync($"https://accounts.spotify.com/authorize?{queryString}");
            var confirmedResponse = response.EnsureSuccessStatusCode();
            
            if ( confirmedResponse == null )
            {
                throw new Exception("Response was not confirmed");
            }

            _webBrowser.OpenDefaultWebBrowserToUrl(confirmedResponse.RequestMessage.RequestUri.ToString());
        }

        public async void Login(SpotifyUserAuthRequest request)
        {
            var client = _spotifyClientFactory.CreateSpotifyClient();
            client.DefaultRequestHeaders.Add("Authorization", request.Headers.Authorization);
            var formJson = new Dictionary<string, string> {
                { "grant_type", request.Form.GrantType},
                { "code", request.Form.Code},
                { "redirect_uri", request.Form.RedirectUri}
            };
            var formData = new FormUrlEncodedContent(formJson);
            var response = await client.PostAsync(request.Uri, formData);
            var content = await response.Content.ReadAsAsync<SpotifyUserAuth>();

            _spotifyDAL.AddAccessToken(content);
        }

        public static string GenerateState(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async Task<string> buildQueryStringAsync(Dictionary<string, string> parameters)
        {
            var dictFormUrlEncoded = new FormUrlEncodedContent(parameters);
            var queryString = await dictFormUrlEncoded.ReadAsStringAsync();
            return queryString;
        }
    }
}
