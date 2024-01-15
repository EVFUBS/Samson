using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SamsonConsoleApp.Actions.WebBrowser
{
    public class WebBrowser : IWebBrowser
    {
        public void OpenDefaultWebBrowser()
        {
            // get this out of config
            string target = "http://www.google.com";
            OpenUrl(target);
        }

        public void OpenDefaultWebBrowserToUrl(string website)
        {
            OpenUrl(website);
        }

        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw new Exception("Could not open web browser!");
                }
            }
        }
    }
}
