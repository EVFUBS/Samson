namespace SamsonConsoleApp.Actions.General.WebBrowser
{
    public interface IWebBrowser
    {
        void OpenDefaultWebBrowser();
        void OpenDefaultWebBrowserToUrl(string website);
    }
}