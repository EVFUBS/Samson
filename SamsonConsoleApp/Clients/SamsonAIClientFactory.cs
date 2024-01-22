using SamsonAIClient;
using SamsonConsoleApp.Clients.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsonConsoleApp.Clients
{
    public class SamsonAIClientFactory : ISamsonAIClientFactory
    {
        public SamsonClient Create()
        {
            // this needs to be something else
            return new SamsonClient("http://127.0.0.1:8000");
        }
    }
}
