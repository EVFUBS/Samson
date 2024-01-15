using Google.Cloud.TextToSpeech.V1;

namespace SamsonConsoleApp.Speech.GoogleTTS
{
    public class GoogleTextToSpeech : IGoogleTextToSpeech
    {
        public async Task CustomTextToSpeech(string summary, string filePath)
        {
            // come back to this later
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
