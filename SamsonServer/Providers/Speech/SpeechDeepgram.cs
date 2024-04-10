using Deepgram;
using Deepgram.Models;
using SamsonServer.Options;

namespace SamsonServer.Providers.Speech
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

        public async Task<PrerecordedTranscription> SpeechToTextFromFile(Stream data)
        {
            var response = await _deepgramClient.Transcription.Prerecorded.GetTranscriptionAsync(
                new StreamSource(data, "audio/wav"),
                new PrerecordedTranscriptionOptions
                {
                    Model = "nova-2",
                    Language = "en",
                    Punctuate = true
                });

            if (response == null)
            {
                throw new Exception("No Response From Deepgram!");
            }

            return response;
        }
    }
}
