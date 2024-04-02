using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SamsonServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeechController : ControllerBase
    {
        // Moving Speech stuff over to the server cuz it makes way more sense (only require 1 set of keys)
        [HttpPost]
        public async Task<IActionResult> Synthesize()
        {
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> SpeechToText()
        {
            return null;
        }
    }
}
