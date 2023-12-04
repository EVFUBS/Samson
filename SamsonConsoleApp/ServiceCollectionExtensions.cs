using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SamsonConsoleApp.Actions;
using SamsonConsoleApp.Actions.Interfaces;
using SamsonConsoleApp.Actions.Spotfiy;
using SamsonConsoleApp.Actions.Spotfiy.Interfaces;
using SamsonConsoleApp.Client;
using SamsonConsoleApp.Clients.Interfaces;
using SamsonConsoleApp.DAL;
using SamsonConsoleApp.DAL.interfaces;
using SamsonConsoleApp.Models;
using SamsonConsoleApp.Models.Interfaces;
using SamsonConsoleApp.Options;
using SamsonConsoleApp.Providers;
using SamsonConsoleApp.Speech;

namespace SamsonConsoleApp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<SpotifyIntegrationOptions>(config.GetSection(nameof(SpotifyIntegrationOptions)));

            services.AddScoped<ISpotifyClientFactory, SpotifyClientFactory>();
            services.AddScoped<IWebBrowser, WebBrowser>();
            services.AddScoped<ISpotifyAuthorisation, SpotifyAuthorisation>();
            services.AddScoped<ISpeechRecognition, SpeechRecognition>();
            services.AddScoped<ISpotifyCredentials, SpotifyCredentials>();
            services.AddScoped<ISpotifyDAL, SpotifyDAL>();
            services.AddScoped<ISpotifyPlayer, SpotifyPlayer>();
            services.AddScoped<ISpotifyAuthProvider, SpotifyAuthProvider>();

            return services;
        }
    }
}
