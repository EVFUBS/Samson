using SamsonConsoleApp.Actions.Spotfiy.Interfaces;
using System.Runtime.InteropServices;
using System.Speech.Recognition;

namespace SamsonConsoleApp.Speech
{
    public class SpeechRecognition : ISpeechRecognition
    {
        private readonly ISpotifyAuthorisation _spotifyIntegration;
        private readonly ISpotifyPlayer _spotifyPlayer;

        public SpeechRecognition(ISpotifyAuthorisation spotifyIntegration, ISpotifyPlayer spotifyPlayer)
        {
            _spotifyIntegration = spotifyIntegration;
            _spotifyPlayer = spotifyPlayer;
        }

        public void RecogniseSpeech()
        {
            _spotifyIntegration.Authorize();
            _spotifyPlayer.PausePlayback();

            //if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            //{
            //    using (
            //   SpeechRecognitionEngine recognizer =
            //     new SpeechRecognitionEngine(
            //       new System.Globalization.CultureInfo("en-US")))
            //    {

            //        // Create and load a dictation grammar.  
            //        recognizer.LoadGrammar(new DictationGrammar());

            //        // Add a handler for the speech recognized event.  
            //        recognizer.SpeechRecognized +=
            //            new EventHandler<SpeechRecognizedEventArgs>(RecognizerSpeechRecognized);

            //        // Configure input to the speech recognizer.  
            //        recognizer.SetInputToDefaultAudioDevice();

            //        // Start asynchronous, continuous speech recognition.  
            //        recognizer.RecognizeAsync(RecognizeMode.Multiple);

            //        while (true)
            //        {
            //            Console.ReadLine();
            //        }
            //    }
            //}
        }

        void RecognizerSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}
