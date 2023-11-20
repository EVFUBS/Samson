using Microsoft.AspNetCore.Mvc;
using SamsonConsoleApp.Actions;
using SamsonConsoleApp.Actions.Interfaces;
using SamsonConsoleApp.Models;
using SamsonConsoleApp.Models.Interfaces;
using System;
using System.Text.Json.Nodes;

namespace SamsonServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpotifyController : Controller
    {
        private readonly ISpotifyCredentials _spotifyCredentials;
        private readonly ISpotifyIntegration _spotifyIntegration;

        public SpotifyController(ISpotifyCredentials spotifyCredentials, ISpotifyIntegration spotifyIntegration)
        {
            _spotifyCredentials = spotifyCredentials;
            _spotifyIntegration = spotifyIntegration;
        }

        [HttpGet("callback")]
        public IActionResult SpotifyAuthoriseCallback([FromQuery] string code, [FromQuery] string state)
        {
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

            _spotifyIntegration.Login(authOptions);

            return Json(authOptions);
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

    }
}
