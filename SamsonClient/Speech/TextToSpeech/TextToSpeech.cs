using Google.Cloud.TextToSpeech.V1;
using SamsonClient.Constants;
using SamsonClient.Speech.Audio;

namespace SamsonClient.Speech.TextToSpeech
{
    public class TextToSpeech : ITextToSpeech
    {
        public async Task Say(string summary)
        {
            await Synthesize(summary, AudioFilePaths.Say);
            AudioPlayer.PlayMp3(AudioFilePaths.Say);
        }

        private async Task Synthesize(string summary, string filePath)
        {
            var client = TextToSpeechClient.Create();
            var response = await client.SynthesizeSpeechAsync(new SynthesizeSpeechRequest
            {
                AudioConfig = new AudioConfig
                {
                    AudioEncoding = AudioEncoding.Mp3
                },
                Voice = new VoiceSelectionParams
                {
                    LanguageCode = "en-US",
                    SsmlGender = SsmlVoiceGender.Male,
                },
                Input = new SynthesisInput
                {
                    Text = summary
                }
            });

            using (Stream output = File.Create(filePath))
            {
                response.AudioContent.WriteTo(output);
            }
        }
    }
}
