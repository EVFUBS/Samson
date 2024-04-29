using SamsonAIClient;
using SamsonConsoleApp.Execute.Spotfiy.Interfaces;
using SamsonConsoleApp.Models.Spotify;
using SamsonConsoleApp.Speech.GoogleTTS;
using Catergories = SamsonCommon.Enums.Catergories;
using Actions = SamsonCommon.Enums.Actions;
using SamsonCommon.Models;
using ActionParameters = SamsonCommon.Models.ActionParameters;

namespace SamsonConsoleApp.Execute.Spotfiy
{
    public class ExecuteSpotify : IExecuteSpotify
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

        public Catergories catergory => Catergories.Spotify;

        public void Execute(SamsonAction action)
        {
            //come back when NER model done to get this to work!
            //var spotifyActionContext = GetSpotifyActionContextFromParameters(action.Parameters);
            switch (action.Action)
            {
                case Actions.SpotifyPlayOrResumePlayback:
                    //if (spotifyActionContext.SongContext.Song != null)
                    //    _player.PlayOrResumePlayback(null, null, null, null);
                    //else
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

        private SpotifyActionContext GetSpotifyActionContextFromParameters(ActionParameters parameters)
        {
            var spotifyActionContext = new SpotifyActionContext();

            foreach (var wordsEntity in parameters.WordEntityPairings)
            {
                if (wordsEntity.Entity == "Play")
                    spotifyActionContext.SongContext = TryBuildSpotifyActionSongContext(parameters);
            }
            return spotifyActionContext;
        }

        private SpotifyActionSongContext TryBuildSpotifyActionSongContext(ActionParameters parameters)
        {
            var spotifyActionSongContext = new SpotifyActionSongContext();
            foreach (var wordEntity in parameters.WordEntityPairings)
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
