using Google.Cloud.TextToSpeech.V1;
using NAudio.Wave;
using SamsonConsoleApp.Constants;

namespace SamsonConsoleApp.Speech.GoogleTTS
{
    public class TextToSpeech : ITextToSpeech
    {
        public async Task Say(string summary)
        {
            await Synthesize(summary, Audio.Say);
            var reader = new Mp3FileReader(Audio.Say);
            var waveOutEvent = new WaveOutEvent();
            waveOutEvent.Init(reader);
            waveOutEvent.Play();
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
