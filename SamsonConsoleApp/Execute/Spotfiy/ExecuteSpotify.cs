using SamsonAIClient;
using SamsonConsoleApp.Execute.Spotfiy.Interfaces;
using SamsonConsoleApp.Enums;
using SamsonConsoleApp.Execute.ExecuteActions;
using SamsonConsoleApp.Models.Samson;
using SamsonConsoleApp.Models.Spotify;
using SamsonConsoleApp.Speech.GoogleTTS;

namespace SamsonConsoleApp.Execute.Spotfiy
{
    public class ExecuteSpotify : IExecuteAction, IExecuteSpotify
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
            var spotifyActionContext = GetSpotifyActionContextFromParameters(action.Parameters);
            switch (action.Action)
            {
                case Actions.SpotifyPlayOrResumePlayback:
                    if (spotifyActionContext.SongContext.Song != null)
                        _player.PlayOrResumePlayback(null, null, null, null);
                    else
                        _player.PlayOrResumePlayback();
                    break;

                case Actions.SpotifyPausePlayback:
                    _player.PausePlayback();
                    break;

                case Actions.SpotifyAvailableDevices:
                    _player.AvailableDevices();
                    break;

                case Actions.DoNotUnderstand:
                default:
                    _textToSpeech.Say("Sorry, I do not understand");
                    break;
            }
        }

        // This is probably going to suck at first but need to give it a go
        private SpotifyActionContext GetSpotifyActionContextFromParameters(SamsonActionParameters parameters)
        {
            var spotifyActionContext = new SpotifyActionContext();

            foreach (var wordsEntity in parameters.WordsEntityPairing)
            {
                if (wordsEntity.Entity == "Play")
                    spotifyActionContext.SongContext = TryBuildSpotifyActionSongContext(parameters);
            }
            return spotifyActionContext;
        }

        private SpotifyActionSongContext TryBuildSpotifyActionSongContext(SamsonActionParameters parameters)
        {
            var spotifyActionSongContext = new SpotifyActionSongContext();
            foreach (var wordEntity in parameters.WordsEntityPairing)
            {
                if (wordEntity.Entity == "Song")
                    spotifyActionSongContext.Song += wordEntity.Word;

                if (wordEntity.Entity == "Artist")
                    spotifyActionSongContext.Artist += wordEntity.Word;
            }
            return spotifyActionSongContext;
        }
    }
}
