using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsonConsoleApp.Options
{
    public class DeepgramIntegrationOptions
    {
        public string ApiKey { get; set; }
        public string ApiUrl { get; set; }
        public bool RequiresSSL { get; set; }
    }
}
