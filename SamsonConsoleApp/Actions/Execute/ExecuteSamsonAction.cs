using SamsonConsoleApp.Actions.General;
using SamsonConsoleApp.Actions.Spotfiy.Interfaces;
using SamsonConsoleApp.Models.Samson;
using SamsonConsoleApp.Speech.GoogleTTS;
using SamsonCatergories = SamsonConsoleApp.Enums.SamsonCatergories;

namespace SamsonConsoleApp.Actions.Execute
{
    public class ExecuteSamsonAction : IExecuteSamsonAction
    {
        private readonly IExecuteGeneral _executeGeneral;
        private readonly IExecuteSpotify _executeSpotify;
        private readonly ITextToSpeech _textToSpeech;

        public ExecuteSamsonAction(
            IExecuteGeneral executeGeneral,
            IExecuteSpotify executeSpotify,
            ITextToSpeech textToSpeech
        ) { 
            _executeGeneral = executeGeneral;
            _executeSpotify = executeSpotify;
            _textToSpeech = textToSpeech;
        }

        public void Execute(SamsonAction action, string summary)
        {
            switch (action.Catergories)
            {
                case SamsonCatergories.General:
                    _executeGeneral.Execute(action, summary);
                    break;

                case SamsonCatergories.Spotify:
                    _executeSpotify.Execute(action, summary);
                    break;

                case SamsonCatergories.DidNotUnderstand:
                default:
                    _textToSpeech.Say("Sorry, I do not understand");
                    break;
            }
        }
    }
}
