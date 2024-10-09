using Microsoft.AspNetCore.Mvc;
using SamsonCommon.Models;
using SamsonServer.Extensions;
using SamsonServer.Providers.Speech;

namespace SamsonServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WakeController(ISpeechProvider speechProvider) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<SamsonWake>> Get(Base64EncodedRequest data)
        {
            var wakeStrings = new[] { "Samson", "Hey Samson", "Hey Simpson", "Hey, Simpson", "Hey, Sam's", "Hey, Sam" };
            var transcribeText = await speechProvider.SpeechToText(data.ToMemoryStream());
            return Ok(new SamsonWake
            {
                IsWake = wakeStrings.Any(substring => transcribeText.Contains(substring))
            });
        }
    }
}