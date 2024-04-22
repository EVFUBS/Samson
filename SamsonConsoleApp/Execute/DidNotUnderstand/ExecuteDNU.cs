using SamsonCommon.Enums;
using SamsonCommon.Models;
using SamsonConsoleApp.Speech.GoogleTTS;

namespace SamsonConsoleApp.Execute.DidNotUnderstand
{
    public class ExecuteDNU(ITextToSpeech textToSpeech) : IExecuteDNU
    {
        public Catergories catergory => Catergories.DidNotUnderstand;

        public void Execute(SamsonAction action)
        {
            textToSpeech.Say("Sorry, I do not understand");
        }
    }
}
