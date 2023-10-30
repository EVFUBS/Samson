using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsonConsoleApp.Actions
{
    public class WebBrowser
    {
        public static void OpenDefaultWebBrowser()
        {
            string target = "http://www.google.com";

            try
            {
                System.Diagnostics.Process.Start(target);
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                // TODO: Have Samson reply here
                if (noBrowser.ErrorCode == -2147467259)
                    Console.WriteLine($"There is no browser installed! {noBrowser}");
                
            }
            catch (System.Exception other)
            {
                Console.WriteLine($"Something really went wrong! {other}");
            }
        }
    }
}
