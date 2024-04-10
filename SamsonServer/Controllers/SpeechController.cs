using Deepgram.Models;
using Microsoft.AspNetCore.Mvc;
using SamsonServer.Providers.Speech;

namespace SamsonServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpeechController(ISpeechDeepgram _deepgram) : ControllerBase
    {
        [HttpPost("synth")]
        public async Task<ActionResult<PrerecordedTranscription>> Synthesize(Stream data)
        {
            try
            {
                var transcript = await _deepgram.SpeechToTextFromFile(data);
                return Ok(transcript);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("tts")]
        public async Task<IActionResult> SpeechToText()
        {
            return Ok();
        }
    }
}
