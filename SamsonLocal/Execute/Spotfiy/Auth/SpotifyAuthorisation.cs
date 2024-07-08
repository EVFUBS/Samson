using SamsonLocal.Execute.General.WebBrowser;
using SamsonLocal.Execute.Spotfiy.Interfaces;
using SamsonLocal.Models.Spotify;
using SamsonLocal.Options;
using SamsonLocal.Providers;
using System.Net;
using System.Text;

namespace SamsonLocal.Execute.Spotfiy.Auth
{
    public class SpotifyAuthorisation(
        IWebBrowser webBrowser,
        ISpotifyIntegrationOptions spotifyIntegrationOptions,
        ISpotifyAuthProvider spotifyAuthProvider)
        : ISpotifyAuthorisation
    {
        public async Task Authorize()
        {
            var client = new HttpClient();
            var state = GenerateState(16);

            var queryParameters = new Dictionary<string, string> {
                { "response_type", spotifyIntegrationOptions.ResponseType },
                { "client_id", spotifyIntegrationOptions.SpotifyClientId },
                { "scope", spotifyIntegrationOptions.Scope },
                { "redirect_uri", spotifyIntegrationOptions.RedirectUri },
                { "state", state },
            };

            var queryString = await BuildQueryStringAsync(queryParameters);
            var response = await client.GetAsync($"https://accounts.spotify.com/authorize?{queryString}");
            var confirmedResponse = response.EnsureSuccessStatusCode();

            if (confirmedResponse == null)
            {
                throw new Exception("Response was not confirmed");
            }

            var spotifyListener = new HttpListener();
            spotifyListener.Prefixes.Add(spotifyIntegrationOptions.RedirectUri + '/');
            spotifyListener.Start();
            try
            {
                webBrowser.OpenDefaultWebBrowserToUrl(confirmedResponse.RequestMessage.RequestUri.ToString());
                var context = await spotifyListener.GetContextAsync();
                AuthorisationCallback(context);
            }
            catch (Exception ex)
            {
                throw new Exception("Error opening web browser, {ex}", ex);
            }
        }

        private void AuthorisationCallback(HttpListenerContext context)
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
                    Code = code ?? throw new InvalidOperationException("code is null"),
                    RedirectUri = spotifyIntegrationOptions.RedirectUri,
                    GrantType = "authorization_code"
                },
                Headers = new SpotifyUserAuthHeaders
                {
                    ContentType = "application/x-www-form-urlencoded",
                    Authorization = "Basic " + Base64Encode(spotifyIntegrationOptions.SpotifyClientId + ":" +
                                                            spotifyIntegrationOptions.SpotifyClientSecret)
                }
            };

            Login(authOptions);
        }

        public async void Login(SpotifyUserAuthRequest request)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", request.Headers.Authorization);
            var formJson = new Dictionary<string, string> {
                { "grant_type", request.Form.GrantType},
                { "code", request.Form.Code},
                { "redirect_uri", request.Form.RedirectUri}
            };
            var formData = new FormUrlEncodedContent(formJson);
            var response = await client.PostAsync(request.Uri, formData);
            var content = await response.Content.ReadFromJsonAsync<SpotifyUserAuth>();

            spotifyAuthProvider.AddSpotifyAccessToken(content ?? throw new InvalidOperationException("response from spotify is null!"));
        }

        public async void RefreshToken(SpotifyUserAuth spotifyUserAuth)
        {
            var client = new HttpClient();
            spotifyAuthProvider.RemoveSpotifyAccessToken(spotifyUserAuth);

            client.DefaultRequestHeaders.Add("Authorization",
                "Basic " + Base64Encode(spotifyIntegrationOptions.SpotifyClientId + ":" +
                                        spotifyIntegrationOptions.SpotifyClientSecret));
            var formJson = new Dictionary<string, string> {
                { "grant_type", "refresh_token"},
                { "refresh_token", spotifyUserAuth.Refresh_token},
            };
            var formData = new FormUrlEncodedContent(formJson);
            var response = await client.PostAsync("https://accounts.spotify.com/api/token", formData);
            var content = await response.Content.ReadFromJsonAsync<SpotifyUserAuth>();

            spotifyAuthProvider.UpdateSpotifyAccessToken(content ?? throw new InvalidOperationException("response from spotify is null!"));
        }

        private static string GenerateState(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async Task<string> BuildQueryStringAsync(Dictionary<string, string> parameters)
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
