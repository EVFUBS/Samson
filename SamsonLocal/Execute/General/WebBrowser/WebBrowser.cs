using Microsoft.Extensions.Configuration;
using SamsonLocal.Options;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SamsonLocal.Execute.General.WebBrowser
{
    public class WebBrowser : IWebBrowser
    {
        private readonly string? _defaultBrowserUrl;

        public WebBrowser(IConfiguration config)
        {
            _defaultBrowserUrl = config?.GetRequiredSection("WebBrowser")?.GetChildren()?.FirstOrDefault(x => x.Key == "defaultWebPage")?.Value;
        }

        public void OpenGoogle()
        {
            OpenUrl("https://google.com");
        }

        public void OpenDefaultWebBrowser()
        {
            if (_defaultBrowserUrl == null)
            {
                OpenUrl("https://google.com");
            }
            OpenUrl(_defaultBrowserUrl);
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
