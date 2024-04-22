using SamsonCommon.Models;
using SamsonConsoleApp.Execute.General;
using SamsonConsoleApp.Execute.General.Greet;
using SamsonConsoleApp.Execute.General.Question;
using SamsonConsoleApp.Execute.General.WebBrowser;
using SamsonConsoleApp.Speech.GoogleTTS;
using Actions = SamsonCommon.Enums.Actions;
using Catergories = SamsonCommon.Enums.Catergories;

namespace SamsonCommon.Execute.General
{
    public class ExecuteGeneral : IExecuteGeneral
    {
        private readonly IGreetAction _greetAction;
        private readonly IQuestionAction _questionAction;
        private readonly IWebBrowser _webBrowser;
        private readonly ITextToSpeech _textToSpeech;

        public ExecuteGeneral(
            IGreetAction greetAction,
            IQuestionAction questionAction,
            IWebBrowser webBrowser,
            ITextToSpeech textToSpeech
        )
        {
            _greetAction = greetAction;
            _questionAction = questionAction;
            _webBrowser = webBrowser;
            _textToSpeech = textToSpeech;
        }

        public Catergories catergory => Catergories.General;

        public void Execute(SamsonAction action)
        {
            switch (action.Action)
            {
                case Actions.Greet:
                    _greetAction.Greeting();
                    break;

                case Actions.Question:
                    _questionAction.Question(action);
                    break;

                case Actions.WebBrowserOpenGoogleBrowser:
                    _webBrowser.OpenDefaultWebBrowser();
                    break;

                case Actions.WebBrowserOpenWebBrowserToUrl:
                    _webBrowser.OpenDefaultWebBrowserToUrl(action.Parameters.WordEntityPairings.FirstOrDefault(x => x.Entity == "URL").Word);
                    break;

                case Actions.DoNotUnderstand:
                default:
                    _textToSpeech.Say("Sorry, I do not understand");
                    break;
            }
        }
    }
}
