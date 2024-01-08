using Microsoft.Extensions.Configuration;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using SamsonConsoleApp.Actions.Interfaces;
using SamsonConsoleApp.Actions.Spotfiy.Constants;
using SamsonConsoleApp.Actions.Spotfiy.Interfaces;
using SamsonConsoleApp.Clients.Interfaces;
using SamsonConsoleApp.DAL.interfaces;
using SamsonConsoleApp.Models.Spotify;
using SamsonConsoleApp.Models.Spotify.Interfaces;
using SamsonConsoleApp.Options;
using SamsonConsoleApp.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SamsonConsoleApp.Actions.Spotfiy
{
    public class SpotifyAuthorisation : ISpotifyAuthorisation
    {
        private readonly ISpotifyClientFactory _spotifyClientFactory;
        private readonly IWebBrowser _webBrowser;
        private readonly ISpotifyCredentials _spotifyCredentials;
        private readonly ISpotifyAuthProvider _spotifyAuthProvider;
        private readonly HttpClient _spotifyClient;

        public SpotifyAuthorisation(
            ISpotifyClientFactory spotifyClientFactory,
            IWebBrowser webBrowser,
            ISpotifyCredentials spotifyCredentials,
            ISpotifyAuthProvider spotifyAuthProvider
            )
        {
            _spotifyClientFactory = spotifyClientFactory;
            _webBrowser = webBrowser;
            _spotifyCredentials = spotifyCredentials;
            _spotifyAuthProvider = spotifyAuthProvider;
            _spotifyClient = _spotifyClientFactory.CreateSpotifyClient();
        }

        public async Task Authorize()
        {
            var state = GenerateState(16);

            var queryParameters = new Dictionary<string, string> {
                { "response_type", _spotifyCredentials.ResponseType },
                { "client_id", _spotifyCredentials.SpotifyClientId },
                { "scope", _spotifyCredentials.Scope },
                { "redirect_uri", _spotifyCredentials.RedirectUri },
                { "state", state },
            };

            var queryString = await buildQueryStringAsync(queryParameters);
            var response = await _spotifyClient.GetAsync($"https://accounts.spotify.com/authorize?{queryString}");
            var confirmedResponse = response.EnsureSuccessStatusCode();

            if (confirmedResponse == null)
            {
                throw new Exception("Response was not confirmed");
            }

            var spotifyListener = new HttpListener();
            spotifyListener.Prefixes.Add(_spotifyCredentials.RedirectUri + '/');
            spotifyListener.Start();
            try
            {
                _webBrowser.OpenDefaultWebBrowserToUrl(confirmedResponse.RequestMessage.RequestUri.ToString());
                var context = await spotifyListener.GetContextAsync();
                AuthoriseCallback(context);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro opening web browser, {ex}", ex);
            }
        }

        public void AuthoriseCallback(HttpListenerContext context)
        {
            var state = context.Request.QueryString["state"];
            var code = context.Request.QueryString["code"];

            if (state == null)
            {
                throw new Exception("state_mismatch");
            }

            var authOptions = new SpotifyUserAuthRequest
            {
                Uri = "https://accounts.spotify.com/api/token",
                Form = new SpotifyUserAuthForm
                {
                    Code = code,
                    RedirectUri = _spotifyCredentials.RedirectUri,
                    GrantType = "authorization_code"
                },
                Headers = new SpotifyUserAuthHeaders
                {
                    ContentType = "application/x-www-form-urlencoded",
                    Authorization = "Basic " + Base64Encode(_spotifyCredentials.SpotifyClientId + ":" + _spotifyCredentials.SpotifyClientSecret)
                }
            };

            Login(authOptions);
        }

        public async void Login(SpotifyUserAuthRequest request)
        {
            _spotifyClient.DefaultRequestHeaders.Add("Authorization", request.Headers.Authorization);
            var formJson = new Dictionary<string, string> {
                { "grant_type", request.Form.GrantType},
                { "code", request.Form.Code},
                { "redirect_uri", request.Form.RedirectUri}
            };
            var formData = new FormUrlEncodedContent(formJson);
            var response = await _spotifyClient.PostAsync(request.Uri, formData);
            var content = await response.Content.ReadAsAsync<SpotifyUserAuth>();

            _spotifyAuthProvider.AddSpotifyAccessToken(content);
            
        }

        public async void RefreshToken(SpotifyUserAuth spotifyUserAuth)
        {
            _spotifyAuthProvider.RemoveSpotifyAccessToken(spotifyUserAuth);

            _spotifyClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Base64Encode(_spotifyCredentials.SpotifyClientId + ":" + _spotifyCredentials.SpotifyClientSecret));
            var formJson = new Dictionary<string, string> {
                { "grant_type", "refresh_token"},
                { "refresh_token", spotifyUserAuth.Refresh_token},
            };
            var formData = new FormUrlEncodedContent(formJson);
            var response = await _spotifyClient.PostAsync("https://accounts.spotify.com/api/token", formData);
            var content = await response.Content.ReadAsAsync<SpotifyUserAuth>();

            _spotifyAuthProvider.AddSpotifyAccessToken(content);
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

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
