using SamsonConsoleApp.Enums;
using SamsonConsoleApp.Execute.ExecuteActions;
using SamsonConsoleApp.Execute.General.Greet;
using SamsonConsoleApp.Execute.General.Question;
using SamsonConsoleApp.Execute.General.WebBrowser;
using SamsonConsoleApp.Models.Samson;
using SamsonConsoleApp.Speech.GoogleTTS;

namespace SamsonConsoleApp.Execute.General
{
    public class ExecuteGeneral : IExecuteAction, IExecuteGeneral
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

        public void Execute(SamsonAction action, string summary)
        {
            switch (action.Action)
            {
                case Actions.Greet:
                    _greetAction.Greeting();
                    break;

                case Actions.Question:
                    _questionAction.Question(summary);
                    break;

                case Actions.WebBrowserOpenGoogleBrowser:
                    _webBrowser.OpenDefaultWebBrowser();
                    break;

                case Actions.WebBrowserOpenWebBrowserToUrl:
                    _webBrowser.OpenDefaultWebBrowserToUrl(action.Parameters.WordsEntityPairing.FirstOrDefault(x => x.Entity == "URL").Word);
                    break;

                case Actions.DoNotUnderstand:
                default:
                    _textToSpeech.Say("Sorry, I do not understand");
                    break;
            }
        }
    }
}
