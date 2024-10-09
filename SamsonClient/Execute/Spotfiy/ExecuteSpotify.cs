using SamsonClient.Execute.Spotfiy.Interfaces;
using SamsonClient.Models.Spotify;
using SamsonClient.Speech.TextToSpeech;
using Catergories = SamsonCommon.Enums.Catergories;
using Actions = SamsonCommon.Enums.Actions;
using SamsonCommon.Models;
using ActionParameters = SamsonCommon.Models.ActionParameters;

namespace SamsonClient.Execute.Spotfiy
{
    public class ExecuteSpotify(
        ISpotifyPlayer player,
        ITextToSpeech textToSpeech) : IExecuteSpotify
    {
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
                        player.PlayOrResumePlayback();
                    break;

                case Actions.SpotifyPausePlayback:
                    player.PausePlayback();
                    break;

                case Actions.SpotifyAvailableDevices:
                    player.AvailableDevices();
                    break;

                case Actions.DoNotUnderstand:
                default:
                    textToSpeech.Say("Sorry, I do not understand");
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
