using System.Speech.Synthesis;

namespace SamsonConsoleApp.Actions
{
    public class Greet
    {
        public static void Greeting()
        {

            try
            {
                var synthesizer = new SpeechSynthesizer();
                synthesizer.SetOutputToDefaultAudioDevice();
                synthesizer.Speak("Hello, these are my first words, nice to meet you, I'm Samson");
            }
            catch
            {
                throw new Exception("Error not using windows");
            }
        }
    }
}
