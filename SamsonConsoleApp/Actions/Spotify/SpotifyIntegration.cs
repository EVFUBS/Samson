using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SamsonConsoleApp.Actions.Interfaces;
using SamsonConsoleApp.Clients.Interfaces;
using SamsonConsoleApp.Models;
using SamsonConsoleApp.Models.Spotify;
using SamsonConsoleApp.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SamsonConsoleApp.Actions.Spotify
{
    public class SpotifyIntegration : ISpotifyIntegration
    {
        private readonly ISpotifyClientFactory _spotifyClientFactory;
        private readonly IWebBrowser _webBrowser;
        private readonly ISpotifyCredentials _spotifyCredentials;

        public SpotifyIntegration(
            ISpotifyClientFactory spotifyClientFactory,
            IWebBrowser webBrowser,
            ISpotifyCredentials spotifyCredentials
            )
        {
            _spotifyClientFactory = spotifyClientFactory;
            _webBrowser = webBrowser;
            _spotifyCredentials = spotifyCredentials;
        }

        public async Task AuthoriseSpotify()
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

            if (confirmedResponse == null)
            {
                throw new Exception("Response was not confirmed");
            }

            _webBrowser.OpenDefaultWebBrowserToUrl(confirmedResponse.RequestMessage.RequestUri.ToString());
        }

        public async Task Login(SpotifyUserAuthRequest spotifyUserAuthRequest)
        {
            var client = _spotifyClientFactory.CreateSpotifyClient();

            var httpContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "code", spotifyUserAuthRequest.form.code },
                { "redirect_uri", spotifyUserAuthRequest.form.redirect_uri },
                { "grant_type", spotifyUserAuthRequest.form.grant_type },
            });
            httpContent.Headers.Add("Content-Type", spotifyUserAuthRequest.headers.content_type);
            httpContent.Headers.Add("Authorization", spotifyUserAuthRequest.headers.Authorization);

            var response = await client.PostAsync(spotifyUserAuthRequest.Url, httpContent);
            var confirmedResponse = response.EnsureSuccessStatusCode();

            if (confirmedResponse == null)
            {
                throw new Exception("Response was not confirmed");
            }

            var responseContent = await confirmedResponse.Content.ReadAsStringAsync();
            var spotifyUserAuthResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<SpotifyUserAuthResponse>(responseContent);
            Console.WriteLine(spotifyUserAuthResponse);
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
