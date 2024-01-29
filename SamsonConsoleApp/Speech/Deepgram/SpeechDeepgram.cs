using Deepgram;
using Deepgram.Models;
using Microsoft.Extensions.Configuration;
using SamsonConsoleApp.Options;

namespace SamsonConsoleApp.Speech.Deepgram
{
    public class SpeechDeepgram : ISpeechDeepgram
    {
        private readonly DeepgramClient _deepgramClient;

        public SpeechDeepgram(IConfiguration config)
        {
            var credentials = config.GetRequiredSection("DeepgramIntegrationOptions").Get<DeepgramIntegrationOptions>();
            _deepgramClient = new DeepgramClient(new Credentials
            {
                ApiKey = credentials?.ApiKey,
                ApiUrl = credentials?.ApiUrl,
                RequireSSL = true
            });
        }


        public async Task<PrerecordedTranscription> SpeechToTextFromFile(string fileUrl)
        {
            using (FileStream stream = File.OpenRead(fileUrl))
            {
                var response = await _deepgramClient.Transcription.Prerecorded.GetTranscriptionAsync(
                    new StreamSource(stream, "audio/wav"),
                    new PrerecordedTranscriptionOptions
                    {
                        Model = "nova-2",
                        Language = "en",
                        Punctuate = true
                    });

                if (response == null)
                {
                    throw new Exception("Response from Deepgram was null!");
                }

                return response;
            }
        }
    }
}
