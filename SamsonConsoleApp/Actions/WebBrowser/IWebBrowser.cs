namespace SamsonConsoleApp.Actions.WebBrowser
{
    public interface IWebBrowser
    {
        void OpenDefaultWebBrowser();
        void OpenDefaultWebBrowserToUrl(string website);
    }
}