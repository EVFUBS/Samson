using Microsoft.Extensions.Configuration;
using SamsonAIClient;
using SamsonConsoleApp.Clients.Interfaces;

namespace SamsonConsoleApp.Clients
{
    public class AiClientFactory : IAiClientFactory
    {
        private readonly IConfiguration _configuration;

        public AiClientFactory(IConfiguration config)
        {
            _configuration = config;
        }

        public SamsonClient Create()
        {
            // this needs to be something else
            return new SamsonClient(_configuration.GetValue<string>("SamsonAi"));
        }
    }
}
