using SamsonAIClient;
using SamsonConsoleApp.Actions.Spotfiy.Interfaces;
using SamsonConsoleApp.Actions.WebBrowser;
using SamsonConsoleApp.enums;
using SamsonConsoleApp.Helpers;

namespace SamsonConsoleApp.Actions.Execute
{
    public class ExecuteSamsonAction : IExecuteSamsonAction
    {
        private readonly ISpotifyPlayer _player;
        private readonly IWebBrowser _webBrowser;

        public ExecuteSamsonAction(
            ISpotifyPlayer player,
            IWebBrowser webBrowser
        ) { 
            _player = player;
            _webBrowser = webBrowser;
        }

        public void Execute(SamsonActionResponse response)
        {
            switch (response.Action.ToEnum<SamsonActions>())
            {
                case SamsonActions.Greet:
                    Greet.Greet.Greeting();
                    break;

                case SamsonActions.Question:
                    break;

                case SamsonActions.WebBrowserOpenWebBrowser:
                    _webBrowser.OpenDefaultWebBrowser();
                    break;

                case SamsonActions.WebBrowserOpenGoogleBrowser:
                    _webBrowser.OpenDefaultWebBrowserToUrl("https://google.com");
                    break;

                case SamsonActions.SpotifyPlayOrResumePlayback:
                    _player.PlayOrResumePlayback();
                    break;

                default:
                    // will want it to say this once tts is integrated
                    Console.WriteLine("Do Not Understand");
                    break;
            }
        }
    }
}
