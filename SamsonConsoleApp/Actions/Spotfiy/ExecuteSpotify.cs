using SamsonAIClient;
using SamsonConsoleApp.Actions.Execute;
using SamsonConsoleApp.Actions.Spotfiy.Interfaces;
using SamsonConsoleApp.Models.Samson;
using SamsonConsoleApp.Models.Spotify;
using SamsonConsoleApp.Speech.GoogleTTS;
using SamsonActions = SamsonConsoleApp.Enums.SamsonActions;

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
            var spotifyActionContext = GetSpotifyActionContextFromParameters(action.Parameters.SpotifyParameters);
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

        // This is probably going to suck at first but need to give it a go
        private SpotifyActionContext GetSpotifyActionContextFromParameters(SamsonSpotifyParameters parameters)
        {
            var spotifyActionContext = new SpotifyActionContext();

            foreach (var wordsEntity in parameters.WordsEntityPairing)
            {
                if (wordsEntity.Entity == "Play")
                    spotifyActionContext.SongContext = TryBuildSpotifyActionSongContext(parameters.WordsEntityPairing);
            }
            return spotifyActionContext;
        }

        private SpotifyActionSongContext TryBuildSpotifyActionSongContext(ICollection<WordsEntity> wordEntities)
        {
            var spotifyActionSongContext = new SpotifyActionSongContext();
            foreach (var wordEntity in wordEntities)
            {
                if (wordEntity.Entity == "Song")
                    spotifyActionSongContext.Song = wordEntity.Words;
                
                if (wordEntity.Entity == "Artist")
                    spotifyActionSongContext.Artist = wordEntity.Words;
            }
            return spotifyActionSongContext;
        }
    }
}
