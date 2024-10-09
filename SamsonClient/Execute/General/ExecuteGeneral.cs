using SamsonClient.Execute.General.Greet;
using SamsonClient.Execute.General.Question;
using SamsonClient.Execute.General.WebBrowser;
using SamsonClient.Speech.TextToSpeech;
using SamsonCommon.Models;
using Actions = SamsonCommon.Enums.Actions;
using Categories = SamsonCommon.Enums.Catergories;

namespace SamsonClient.Execute.General
{
    public class ExecuteGeneral(
        IGreetAction greetAction,
        IQuestionAction questionAction,
        IWebBrowser webBrowser,
        ITextToSpeech textToSpeech)
        : IExecuteGeneral
    {
        public Categories catergory => Categories.General;

        public void Execute(SamsonAction action)
        {
            switch (action.Action)
            {
                case Actions.Greet:
                    greetAction.Greeting();
                    break;

                case Actions.Question:
                    questionAction.Question(action);
                    break;

                case Actions.OpenGoogleBrowser:
                    webBrowser.OpenGoogle();
                    break;

                case Actions.OpenWebBrowser:
                    webBrowser.OpenDefaultWebBrowserToUrl(action.Parameters.WordEntityPairings.FirstOrDefault(x => x.Entity == "URL").Word);
                    break;

                case Actions.SpotifyAvailableDevices:
                case Actions.SpotifyPlayOrResumePlayback:
                case Actions.SpotifyPausePlayback:
                case Actions.SpotifyStartPlaylist:
                case Actions.DoNotUnderstand:
                default:
                    textToSpeech.Say("Sorry, I do not understand");
                    break;
            }
        }
    }
}
