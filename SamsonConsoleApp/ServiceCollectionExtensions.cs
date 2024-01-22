using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SamsonConsoleApp.Actions.General.WebBrowser;
using SamsonConsoleApp.Client;
using SamsonConsoleApp.Clients.Interfaces;
using SamsonConsoleApp.Options;
using SamsonConsoleApp.Speech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsonConsoleApp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<SpotifyIntegrationOptions>(config.GetSection(nameof(SpotifyIntegrationOptions)));

            services.AddScoped<ISpotifyClientFactory, SpotifyClientFactory>();
            services.AddScoped<IWebBrowser, WebBrowser>();

            services.AddSingleton<ISpeechRecognition, SpeechRecognition>();

            return services;
        }
    }
}
