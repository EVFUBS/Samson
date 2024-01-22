using SamsonConsoleApp.Actions.Execute;
using SamsonConsoleApp.Actions.Spotfiy.Interfaces;
using SamsonConsoleApp.enums;
using SamsonConsoleApp.Models.Samson;
using SamsonConsoleApp.Speech.GoogleTTS;

namespace SamsonConsoleApp.Actions.Spotfiy
{
    public class ExecuteSpotify : IExecuteSamsonAction, IExecuteSpotify
    {
        private readonly ISpotifyPlayer _player;
        private readonly ITextToSpeech _textToSpeech;

        public ExecuteSpotify(
            ISpotifyPlayer player,
            ITextToSpeech textToSpeech
        )
        {
            _player = player;
            _textToSpeech = textToSpeech;
        }

        public void Execute(SamsonAction action, string summary)
        {
            switch (action.Action)
            {
                case SamsonActions.SpotifyPlayOrResumePlayback:
                    _player.PlayOrResumePlayback();
                    break;

                case SamsonActions.SpotifyPausePlayback:
                    _player.PausePlayback();
                    break;

                case SamsonActions.SpotifyAvailableDevices:
                    _player.AvailableDevices();
                    break;

                case SamsonActions.DoNotUnderstand:
                default:
                    _textToSpeech.Say("Sorry, I do not understand");
                    break;
            }
        }
    }
}
