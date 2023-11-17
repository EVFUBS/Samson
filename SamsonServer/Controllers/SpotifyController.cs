using Microsoft.AspNetCore.Mvc;
using SamsonConsoleApp.Actions;
using SamsonConsoleApp.Actions.Interfaces;
using SamsonConsoleApp.Models.Spotify;
using System;
using System.Text.Json.Nodes;

namespace SamsonServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpotifyController : Controller
    {
        ISpotifyCredentials _spotifyCredentials;
        ISpotifyIntegration _spotifyIntegration;
        
        public SpotifyController(ISpotifyCredentials spotifyCredentials, ISpotifyIntegration spotifyIntegration)
        {
            _spotifyIntegration = spotifyIntegration;
            _spotifyCredentials = spotifyCredentials;
        }

        [HttpGet("callback")]
        public void SpotifyAuthoriseCallback([FromQuery] string code, [FromQuery] string state)
        {
            Console.WriteLine("test");

            if (state == null)
            {
                // Implement this properly later
                throw new Exception("state_mismatch");
            }

            var spotifyUserAuthRequest = new SpotifyUserAuthRequest
            {
                Url = "https://accounts.spotify.com/api/token",
                form = new SpotifyForm
                {
                    code = code,
                    redirect_uri = _spotifyCredentials.RedirectUri,
                    grant_type = "authorization_code"
                },
                headers = new SpotifyHeader
                {
                    content_type = "application/x-www-form-urlencoded",
                    Authorization = "Basic " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_spotifyCredentials.SpotifyClientId + ":" + _spotifyCredentials.SpotifyClientSecret))
                }
            };
            
            _spotifyIntegration.Login(spotifyUserAuthRequest);
        }
    }
}
