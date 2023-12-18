using Deepgram;
using Deepgram.Models;
using Microsoft.Extensions.Configuration;
using SamsonConsoleApp.Options;

namespace SamsonConsoleApp.Speech.Deepgram
{
    public class Deepgram
    {
        private readonly DeepgramClient _deepgramClient;

        public Deepgram(IConfiguration config)
        {
            var credentials = config.GetRequiredSection("DeepgramIntegration").Get<DeepgramIntegrationOptions>();
            _deepgramClient = new DeepgramClient(new Credentials
            {
                ApiKey = credentials?.ApiKey,
                ApiUrl = credentials?.ApiUrl,
                RequireSSL = true
            });
        }


        public async Task<PrerecordedTranscription> SpeechToTextFromFile(string fileUrl)
        {
            DeepgramClient client = new DeepgramClient();
            using (FileStream stream = File.OpenRead(fileUrl))
            {
                var response = await client.Transcription.Prerecorded.GetTranscriptionAsync(
                    new StreamSource(stream, "audio/wav"), 
                    new PrerecordedTranscriptionOptions
                    {
                        Model = "nova-2",
                        Punctuate = true
                    });
                return response;
            }
        }
    }
}
