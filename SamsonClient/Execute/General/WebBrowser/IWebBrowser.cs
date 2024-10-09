namespace SamsonClient.Execute.General.WebBrowser
{
    public interface IWebBrowser
    {
        void OpenDefaultWebBrowser();
        void OpenDefaultWebBrowserToUrl(string website);
        void OpenGoogle();
    }
}