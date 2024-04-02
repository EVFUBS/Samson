using SamsonConsoleApp.Execute.Spotfiy.Interfaces;
using SamsonConsoleApp.Execute.General;
using SamsonConsoleApp.Models.Samson;
using SamsonConsoleApp.Speech.GoogleTTS;
using Catergories = SamsonConsoleApp.Enums.Catergories;

namespace SamsonConsoleApp.Execute.ExecuteActions
{
    public class ExecuteAction : IExecuteAction
    {
        private readonly IExecuteGeneral _executeGeneral;
        private readonly IExecuteSpotify _executeSpotify;
        private readonly ITextToSpeech _textToSpeech;

        public ExecuteAction(
            IExecuteGeneral executeGeneral,
            IExecuteSpotify executeSpotify,
            ITextToSpeech textToSpeech
        )
        {
            _executeGeneral = executeGeneral;
            _executeSpotify = executeSpotify;
            _textToSpeech = textToSpeech;
        }

        public void Execute(SamsonAction action, string summary)
        {
            switch (action.Catergories)
            {
                case Catergories.General:
                    _executeGeneral.Execute(action, summary);
                    break;

                case Catergories.Spotify:
                    _executeSpotify.Execute(action, summary);
                    break;

                case Catergories.DidNotUnderstand:
                default:
                    _textToSpeech.Say("Sorry, I do not understand");
                    break;
            }
        }
    }
}
