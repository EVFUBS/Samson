using Microsoft.AspNetCore.Mvc;
using SamsonConsoleApp.Actions;
using SamsonConsoleApp.Models;
using System;
using System.Text.Json.Nodes;

namespace SamsonServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpotifyController : Controller
    {
        ISpotifyCredentials _spotifyCredentials;
        public SpotifyController(ISpotifyCredentials spotifyCredentials)
        {
            _spotifyCredentials = spotifyCredentials;
        }

        [HttpGet("callback")]
        public IActionResult SpotifyAuthoriseCallback([FromQuery] string code, [FromQuery] string state)
        {
            Console.WriteLine("test");

            if (state == null)
            {
                // Implement this properly later
                throw new Exception("state_mismatch");
            }

            var authOptions = new JsonObject
            {
                { "url", "https://accounts.spotify.com/api/token" },
                { "form",  new JsonObject{ 
                    { "code", code },
                    { "redirect_uri", _spotifyCredentials.RedirectUri },
                    { "grant_type", "authorisation_code" }
                }},
                { "headers", new JsonObject
                {
                    { "content-type", "application/x-www-form-urlencoded" },
                    { "Authorization", "Basic " + _spotifyCredentials.SpotifyClientId + ":" + _spotifyCredentials.SpotifyClientSecret }
                }},
                { "json", true }
            };



            return Json(authOptions);
        }
    }
}
