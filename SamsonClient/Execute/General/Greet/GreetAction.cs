using System.Speech.Synthesis;
using SamsonClient.Speech.TextToSpeech;

namespace SamsonClient.Execute.General.Greet
{
    public class GreetAction : IGreetAction
    {
        private ITextToSpeech _textToSpeech;

        public GreetAction(
            ITextToSpeech textToSpeech
        )
        {
            _textToSpeech = textToSpeech;
        }

        public void Greeting()
        {
            // Will want a couple of pre-recorded greetings here - saves using API over and over again and spending money :/
            Console.WriteLine("Temporary Hi");
            //_textToSpeech.Say("Hi");
        }
    }
}
