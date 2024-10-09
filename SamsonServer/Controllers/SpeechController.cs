using AutoMapper;
using Deepgram.Models;
using Microsoft.AspNetCore.Mvc;
using SamsonServer.Extensions;
using SamsonServer.Models.ReturnModels.Speech;
using SamsonServer.Providers.Speech;
using Whisper.net;
using Whisper.net.Ggml;


namespace SamsonServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpeechController(ISpeechDeepgram deepgram, ISpeechProvider speechProvider) : ControllerBase
    {
        [HttpPost("synth")]
        public async Task<ActionResult<PrerecordedTranscription>> DeepgramSpeechToText(Stream data)
        {
            try
            {
                var transcript = await deepgram.SpeechToTextFromFile(data);
                return Ok(transcript);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("stt")]
        public async Task<ActionResult<SpeechToText>> SpeechToText(Base64EncodedRequest data)
        {
            return Ok(new SpeechToText
            {
                Text = await speechProvider.SpeechToText(data.ToMemoryStream())
            });
        }

        [HttpPost("tts")]
        public async Task<ActionResult> TextToSpeech(string text)
        {
            return Ok("oke");
        }
    }
}
