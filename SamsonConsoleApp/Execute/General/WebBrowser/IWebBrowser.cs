namespace SamsonConsoleApp.Execute.General.WebBrowser
{
    public interface IWebBrowser
    {
        void OpenDefaultWebBrowser();
        void OpenDefaultWebBrowserToUrl(string website);
    }
}