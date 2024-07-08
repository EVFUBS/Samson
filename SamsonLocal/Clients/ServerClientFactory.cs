using Microsoft.Extensions.Configuration;
using SamsonLocal.Clients.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsonLocal.Clients
{
    public class ServerClientFactory : IServerClientFactory
    {
        private readonly IConfiguration _configuration;

        public ServerClientFactory(IConfiguration config)
        {
            _configuration = config;
        }

        public SamsonServerClient.SamsonServerClient Create()
        {
            // this needs to be something else
            return new SamsonServerClient.SamsonServerClient(_configuration.GetSection("SamsonServer").GetValue<string>("SamsonServerUrl"));
        }
    }
}
