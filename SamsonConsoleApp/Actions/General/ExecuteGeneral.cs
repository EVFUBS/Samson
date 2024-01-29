using SamsonConsoleApp.Actions.Execute;
using SamsonConsoleApp.Actions.General.Greet;
using SamsonConsoleApp.Actions.General.Question;
using SamsonConsoleApp.Actions.General.WebBrowser;
using SamsonConsoleApp.Models.Samson;
using SamsonConsoleApp.Speech.GoogleTTS;
using SamsonActions = SamsonConsoleApp.Enums.SamsonActions;

namespace SamsonConsoleApp.Actions.General
{
    public class ExecuteGeneral : IExecuteSamsonAction, IExecuteGeneral
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
                case SamsonActions.Greet:
                    _greetAction.Greeting();
                    break;

                case SamsonActions.Question:
                    _questionAction.Question(summary);
                    break;

                case SamsonActions.WebBrowserOpenGoogleBrowser:
                    _webBrowser.OpenDefaultWebBrowser();
                    break;

                case SamsonActions.WebBrowserOpenWebBrowserToUrl:
                    // want you to be able to specify the site you want to go to
                    _webBrowser.OpenDefaultWebBrowserToUrl("https://google.com");
                    break;

                case SamsonActions.DoNotUnderstand:
                default:
                    _textToSpeech.Say("Sorry, I do not understand");
                    break;
            }
        }
    }
}
