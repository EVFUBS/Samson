using SamsonClient.Speech.TextToSpeech;
using SamsonCommon.Enums;
using SamsonCommon.Models;

namespace SamsonClient.Execute.DidNotUnderstand
{
    public class ExecuteDnu(ITextToSpeech textToSpeech) : IExecuteDnu
    {
        public Catergories catergory => Catergories.DidNotUnderstand;

        public void Execute(SamsonAction action)
        {
            textToSpeech.Say("Sorry, I do not understand");
        }
    }
}
